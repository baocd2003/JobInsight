using System;
using System.Collections.Generic;

namespace JobInsight.CV
{
    public class AnalyseCvInput
    {
        /// <summary>
        /// Target job title user wants to be analysed against (e.g. "Backend Developer")
        /// </summary>
        public string TargetJobTitle { get; set; }
    }

    public class CvAnalysisResultDto
    {
        public Guid CvAnalysisId { get; set; }
        public string TargetJobTitle { get; set; }
        public decimal MarketMatchScore { get; set; }
        public int ReferencedJobCount { get; set; }
        public List<string> Strengths { get; set; } = new();
        public List<string> Weaknesses { get; set; } = new();
        public List<MissingSkillDto> MissingSkills { get; set; } = new();
        public List<UserSkillResultDto> ExtractedSkills { get; set; } = new();
        public DateTime AnalysedAt { get; set; }
    }

    public class MissingSkillDto
    {
        public Guid? SkillId { get; set; }
        public string SkillName { get; set; }
        public string Priority { get; set; }       // High / Medium / Low
        public int JobMentionCount { get; set; }
        public decimal FrequencyPercent { get; set; }
    }

    public class UserSkillResultDto
    {
        public Guid? SkillId { get; set; }
        public string SkillName { get; set; }
        public string ProficiencyLevel { get; set; }
    }
}
