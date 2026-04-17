using System;
using Volo.Abp.Application.Dtos;

namespace JobInsight.Entities.Dtos
{
    /// <summary>
    /// DTO for creating/updating a skill
    /// </summary>
    public class CreateUpdateSkillDto
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Aliases { get; set; }
        public string RelatedSkills { get; set; }
        public string IconUrl { get; set; }
        public string Color { get; set; }
    }

    /// <summary>
    /// DTO for reading skill data
    /// </summary>
    public class SkillDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Aliases { get; set; }
        public string RelatedSkills { get; set; }
        public int TotalJobMentions { get; set; }
        public decimal TrendingScore { get; set; }
        public string IconUrl { get; set; }
        public string Color { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
