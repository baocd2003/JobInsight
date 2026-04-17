using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace JobInsight.Entities
{
    /// <summary>
    /// Stores the AI analysis result of a CV compared against the current job market
    /// </summary>
    public class CvAnalysis : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// ID of the CV upload that was analyzed
        /// </summary>
        public Guid CvUploadId { get; set; }

        /// <summary>
        /// ID of the user
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The job title/role this analysis targets (e.g., "Backend Developer", "Data Engineer")
        /// </summary>
        public string TargetJobTitle { get; set; }

        /// <summary>
        /// JSON array of identified strengths (e.g., ["Strong C# experience", "Microservices background"])
        /// </summary>
        public string Strengths { get; set; }

        /// <summary>
        /// JSON array of identified weaknesses (e.g., ["No cloud experience", "Missing CI/CD knowledge"])
        /// </summary>
        public string Weaknesses { get; set; }

        /// <summary>
        /// JSON array of missing skills with priority and market demand
        /// Format: [{ "skillId": "...", "skillName": "...", "priority": "High", "jobMentionCount": 120 }]
        /// </summary>
        public string MissingSkills { get; set; }

        /// <summary>
        /// Percentage score indicating how well the CV matches current market requirements (0-100)
        /// </summary>
        public decimal MarketMatchScore { get; set; }

        /// <summary>
        /// Number of recent job postings used as reference for this analysis
        /// </summary>
        public int ReferencedJobCount { get; set; }

        /// <summary>
        /// Date range start of job postings used as reference
        /// </summary>
        public DateTime? ReferenceJobsFrom { get; set; }

        /// <summary>
        /// Date range end of job postings used as reference
        /// </summary>
        public DateTime? ReferenceJobsTo { get; set; }

        /// <summary>
        /// Raw AI response stored for debugging or re-parsing
        /// </summary>
        public string RawAiResponse { get; set; }

        /// <summary>
        /// AI model used for this analysis (e.g., "claude-sonnet-4-6")
        /// </summary>
        public string AiModel { get; set; }

        /// <summary>
        /// When the analysis was completed
        /// </summary>
        public DateTime AnalyzedAt { get; set; }

        // Navigation properties
        public virtual CvUpload CvUpload { get; set; }
        public virtual AppUser User { get; set; }

        public CvAnalysis()
        {
        }

        public CvAnalysis(Guid cvUploadId, Guid userId, string targetJobTitle)
        {
            CvUploadId = cvUploadId;
            UserId = userId;
            TargetJobTitle = targetJobTitle;
            AnalyzedAt = DateTime.UtcNow;
        }
    }
}
