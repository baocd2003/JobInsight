using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace JobInsight.Entities
{
    /// <summary>
    /// Junction entity for Job-Skill many-to-many relationship
    /// </summary>
    public class JobSkill : CreationAuditedEntity<Guid>
    {
        /// <summary>
        /// Job ID
        /// </summary>
        public Guid JobId { get; set; }

        /// <summary>
        /// Skill ID
        /// </summary>
        public Guid SkillId { get; set; }

        /// <summary>
        /// Whether this skill is required for the job
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Whether this is a primary/main skill for the job
        /// </summary>
        public bool IsPrimarySkill { get; set; }

        /// <summary>
        /// Required proficiency level (e.g., "Basic", "Intermediate", "Advanced", "Expert")
        /// </summary>
        public string ProficiencyLevel { get; set; }

        /// <summary>
        /// Number of times this skill was mentioned in the job posting
        /// </summary>
        public int MentionCount { get; set; } = 1;

        // Navigation properties
        public virtual Job Job { get; set; }
        public virtual Skill Skill { get; set; }

        public JobSkill()
        {
        }

        public JobSkill(Guid jobId, Guid skillId)
        {
            JobId = jobId;
            SkillId = skillId;
        }
    }
}
