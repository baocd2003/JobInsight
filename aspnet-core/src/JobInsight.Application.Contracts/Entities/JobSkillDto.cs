using System;
using Volo.Abp.Application.Dtos;

namespace JobInsight.Entities.Dtos
{
    /// <summary>
    /// DTO for job-skill relationship
    /// </summary>
    public class CreateUpdateJobSkillDto
    {
        public Guid JobId { get; set; }
        public Guid SkillId { get; set; }
        public bool IsRequired { get; set; }
        public bool IsPrimarySkill { get; set; }
        public string ProficiencyLevel { get; set; }
    }

    /// <summary>
    /// DTO for reading job-skill data
    /// </summary>
    public class JobSkillDto : EntityDto<Guid>
    {
        public Guid JobId { get; set; }
        public Guid SkillId { get; set; }
        public string SkillName { get; set; }
        public bool IsRequired { get; set; }
        public bool IsPrimarySkill { get; set; }
        public string ProficiencyLevel { get; set; }
        public int MentionCount { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
