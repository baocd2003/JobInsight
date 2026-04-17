using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JobInsight.CV;
using JobInsight.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace JobInsight.Services.CV
{
    public class CvAppService : ApplicationService, ICvAppService
    {
        private readonly IRepository<Job, Guid> _jobRepository;
        private readonly IRepository<JobSkill, Guid> _jobSkillRepository;
        private readonly IRepository<Skill, Guid> _skillRepository;
        private readonly IRepository<CvAnalysis, Guid> _cvAnalysisRepository;
        private readonly IRepository<UserSkill, Guid> _userSkillRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<CvAppService> _logger;

        private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".pdf", ".docx", ".doc", ".txt"
        };

        public CvAppService(
            IRepository<Job, Guid> jobRepository,
            IRepository<JobSkill, Guid> jobSkillRepository,
            IRepository<Skill, Guid> skillRepository,
            IRepository<CvAnalysis, Guid> cvAnalysisRepository,
            IRepository<UserSkill, Guid> userSkillRepository,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ICurrentUser currentUser,
            ILogger<CvAppService> logger)
        {
            _jobRepository = jobRepository;
            _jobSkillRepository = jobSkillRepository;
            _skillRepository = skillRepository;
            _cvAnalysisRepository = cvAnalysisRepository;
            _userSkillRepository = userSkillRepository;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _currentUser = currentUser;
            _logger = logger;
        }

        public async Task<CvAnalysisResultDto> UploadAndAnalyseAsync(IFormFile file, AnalyseCvInput input)
        {
            // 1. Validate file
            ValidateFile(file);

            // 2. Extract raw text from file (in-memory, no disk write)
            var cvText = await ExtractTextAsync(file);
            if (string.IsNullOrWhiteSpace(cvText))
                throw new UserFriendlyException("Could not extract text from the uploaded file.");

            // 3. Query market data from DB
            var marketSkills = await GetMarketSkillsAsync(input.TargetJobTitle);

            // 4. Call Python AI service
            var aiResult = await CallAiServiceAsync(cvText, input.TargetJobTitle, marketSkills);

            // 5. Match extracted skill names to Skill entities in DB
            var allSkills = await _skillRepository.GetListAsync();
            var extractedSkills = MatchSkills(aiResult.ExtractedSkills, allSkills);
            var missingSkills = BuildMissingSkills(aiResult.MissingSkills, allSkills, marketSkills);

            // 6. Persist CvAnalysis
            var userId = _currentUser.GetId();
            var analysis = new CvAnalysis(Guid.NewGuid(), userId, input.TargetJobTitle)
            {
                Strengths = JsonSerializer.Serialize(aiResult.Strengths),
                Weaknesses = JsonSerializer.Serialize(aiResult.Weaknesses),
                MissingSkills = JsonSerializer.Serialize(missingSkills),
                MarketMatchScore = aiResult.MarketMatchScore,
                ReferencedJobCount = marketSkills.Count,
                ReferenceJobsFrom = DateTime.UtcNow.AddDays(-90),
                ReferenceJobsTo = DateTime.UtcNow,
                RawAiResponse = aiResult.RawResponse,
                AiModel = aiResult.ModelUsed,
                AnalyzedAt = DateTime.UtcNow
            };
            await _cvAnalysisRepository.InsertAsync(analysis, autoSave: true);

            // 7. Upsert UserSkills
            await UpsertUserSkillsAsync(userId, extractedSkills, analysis.Id);

            return new CvAnalysisResultDto
            {
                CvAnalysisId = analysis.Id,
                TargetJobTitle = input.TargetJobTitle,
                MarketMatchScore = analysis.MarketMatchScore,
                ReferencedJobCount = analysis.ReferencedJobCount,
                Strengths = aiResult.Strengths,
                Weaknesses = aiResult.Weaknesses,
                MissingSkills = missingSkills,
                ExtractedSkills = extractedSkills,
                AnalysedAt = analysis.AnalyzedAt
            };
        }

        // ─── Helpers ────────────────────────────────────────────────────────────────

        private static void ValidateFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new UserFriendlyException("No file uploaded.");

            if (file.Length > 5 * 1024 * 1024)
                throw new UserFriendlyException("File size must not exceed 5MB.");

            var ext = Path.GetExtension(file.FileName);
            if (!AllowedExtensions.Contains(ext))
                throw new UserFriendlyException("Only PDF, DOCX, DOC, and TXT files are supported.");
        }

        private static async Task<string> ExtractTextAsync(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            await using var stream = file.OpenReadStream();

            if (ext == ".pdf")
                return ExtractPdfText(stream);

            if (ext is ".txt")
            {
                using var reader = new StreamReader(stream, Encoding.UTF8);
                return await reader.ReadToEndAsync();
            }

            // .docx / .doc — read raw bytes and extract printable text as fallback
            // For production: add DocumentFormat.OpenXml package for proper DOCX parsing
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            return Encoding.UTF8.GetString(ms.ToArray()
                .Where(b => b >= 32 && b < 127)
                .ToArray());
        }

        private static string ExtractPdfText(Stream stream)
        {
            using var pdf = PdfDocument.Open(stream);
            var sb = new StringBuilder();
            foreach (Page page in pdf.GetPages())
                sb.AppendLine(page.Text);
            return sb.ToString();
        }

        private async Task<List<MarketSkillContext>> GetMarketSkillsAsync(string targetJobTitle)
        {
            var cutoff = DateTime.UtcNow.AddDays(-90);

            // Get recent active jobs matching the title
            var jobs = await _jobRepository.GetListAsync(
                j => j.IsActive &&
                     j.PostedDate >= cutoff &&
                     j.Title.ToLower().Contains(targetJobTitle.ToLower()));

            if (!jobs.Any())
                return new List<MarketSkillContext>();

            var jobIds = jobs.Select(j => j.Id).ToList();
            var totalJobs = jobs.Count;

            // Get all skills for those jobs
            var jobSkills = await _jobSkillRepository.GetListAsync(js => jobIds.Contains(js.JobId));
            var skillIds = jobSkills.Select(js => js.SkillId).Distinct().ToList();
            var skills = await _skillRepository.GetListAsync(s => skillIds.Contains(s.Id));

            var skillMap = skills.ToDictionary(s => s.Id);

            return jobSkills
                .GroupBy(js => js.SkillId)
                .Select(g => new MarketSkillContext
                {
                    SkillId = g.Key,
                    SkillName = skillMap.TryGetValue(g.Key, out var s) ? s.Name : null,
                    MentionCount = g.Count(),
                    FrequencyPercent = Math.Round((decimal)g.Count() / totalJobs * 100, 1),
                    IsRequired = g.Any(js => js.IsRequired)
                })
                .OrderByDescending(x => x.MentionCount)
                .Take(50)
                .ToList();
        }

        private async Task<AiServiceResponse> CallAiServiceAsync(
            string cvText,
            string targetJobTitle,
            List<MarketSkillContext> marketSkills)
        {
            var aiServiceUrl = _configuration["AiService:BaseUrl"]?.TrimEnd('/') ?? "http://localhost:8000";

            var payload = new
            {
                cv_text = cvText,
                target_job_title = targetJobTitle,
                market_skills = marketSkills.Select(s => new
                {
                    skill_name = s.SkillName,
                    mention_count = s.MentionCount,
                    frequency_percent = s.FrequencyPercent,
                    is_required = s.IsRequired
                })
            };

            var client = _httpClientFactory.CreateClient("AiService");
            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json");

            HttpResponseMessage response;
            try
            {
                response = await client.PostAsync($"{aiServiceUrl}/analyse", content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to reach AI service at {Url}", aiServiceUrl);
                throw new UserFriendlyException("AI service is unavailable. Please try again later.");
            }

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError("AI service returned {Status}: {Error}", response.StatusCode, error);
                throw new UserFriendlyException("AI service failed to analyse the CV.");
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AiServiceResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        private static List<UserSkillResultDto> MatchSkills(
            List<AiExtractedSkill> extracted,
            List<Skill> allSkills)
        {
            return extracted.Select(e =>
            {
                var matched = FindSkillMatch(e.SkillName, allSkills);
                return new UserSkillResultDto
                {
                    SkillId = matched?.Id,
                    SkillName = matched?.Name ?? e.SkillName,
                    ProficiencyLevel = e.ProficiencyLevel
                };
            }).ToList();
        }

        private static List<MissingSkillDto> BuildMissingSkills(
            List<AiMissingSkill> aiMissing,
            List<Skill> allSkills,
            List<MarketSkillContext> marketSkills)
        {
            return aiMissing.Select(m =>
            {
                var matched = FindSkillMatch(m.SkillName, allSkills);
                var marketCtx = marketSkills.FirstOrDefault(ms =>
                    string.Equals(ms.SkillName, m.SkillName, StringComparison.OrdinalIgnoreCase));

                return new MissingSkillDto
                {
                    SkillId = matched?.Id,
                    SkillName = matched?.Name ?? m.SkillName,
                    Priority = m.Priority,
                    JobMentionCount = marketCtx?.MentionCount ?? 0,
                    FrequencyPercent = marketCtx?.FrequencyPercent ?? 0
                };
            }).ToList();
        }

        private static Skill FindSkillMatch(string name, List<Skill> allSkills)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;
            var normalized = name.ToUpperInvariant().Replace(" ", "").Replace(".", "").Replace("#", "SHARP");

            // 1. Exact normalized name match
            var match = allSkills.FirstOrDefault(s =>
                s.NormalizedName?.Equals(normalized, StringComparison.OrdinalIgnoreCase) == true);
            if (match != null) return match;

            // 2. Alias match (Aliases is JSON array stored as string)
            match = allSkills.FirstOrDefault(s =>
            {
                if (string.IsNullOrWhiteSpace(s.Aliases)) return false;
                try
                {
                    var aliases = JsonSerializer.Deserialize<List<string>>(s.Aliases);
                    return aliases?.Any(a =>
                        a.Replace(" ", "").Replace(".", "")
                         .Equals(name.Replace(" ", "").Replace(".", ""),
                                 StringComparison.OrdinalIgnoreCase)) == true;
                }
                catch { return false; }
            });
            if (match != null) return match;

            // 3. Display name contains match
            return allSkills.FirstOrDefault(s =>
                s.Name?.Contains(name, StringComparison.OrdinalIgnoreCase) == true ||
                name.Contains(s.Name ?? "", StringComparison.OrdinalIgnoreCase));
        }

        private async Task UpsertUserSkillsAsync(
            Guid userId,
            List<UserSkillResultDto> extractedSkills,
            Guid cvAnalysisId)
        {
            var existing = await _userSkillRepository.GetListAsync(us => us.UserId == userId);

            foreach (var skill in extractedSkills.Where(s => s.SkillId.HasValue))
            {
                var exists = existing.FirstOrDefault(e => e.SkillId == skill.SkillId!.Value);
                if (exists != null)
                {
                    // Update proficiency if AI returned a better reading
                    exists.ProficiencyLevel = skill.ProficiencyLevel;
                    await _userSkillRepository.UpdateAsync(exists);
                }
                else
                {
                    await _userSkillRepository.InsertAsync(new UserSkill(userId, skill.SkillId!.Value, "CV")
                    {
                        ProficiencyLevel = skill.ProficiencyLevel,
                        CvUploadId = null  // not storing file
                    });
                }
            }
        }

        // ─── Internal DTOs (AI service contract) ────────────────────────────────────

        private class MarketSkillContext
        {
            public Guid SkillId { get; set; }
            public string SkillName { get; set; }
            public int MentionCount { get; set; }
            public decimal FrequencyPercent { get; set; }
            public bool IsRequired { get; set; }
        }

        private class AiServiceResponse
        {
            public List<string> Strengths { get; set; } = new();
            public List<string> Weaknesses { get; set; } = new();
            public List<AiExtractedSkill> ExtractedSkills { get; set; } = new();
            public List<AiMissingSkill> MissingSkills { get; set; } = new();
            public decimal MarketMatchScore { get; set; }
            public string RawResponse { get; set; }
            public string ModelUsed { get; set; }
        }

        private class AiExtractedSkill
        {
            public string SkillName { get; set; }
            public string ProficiencyLevel { get; set; }
        }

        private class AiMissingSkill
        {
            public string SkillName { get; set; }
            public string Priority { get; set; }
        }
    }
}
