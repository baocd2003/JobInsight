using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace JobInsight.Entities
{
    /// <summary>
    /// Represents a skill belonging to a user, extracted from CV or added manually
    /// </summary>
    public class UserSkill : CreationAuditedEntity<Guid>
    {
        /// <summary>
        /// ID of the user
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// ID of the skill
        /// </summary>
        public Guid SkillId { get; set; }

        /// <summary>
        /// Self-assessed or AI-assessed proficiency level (e.g., "Basic", "Intermediate", "Advanced", "Expert")
        /// </summary>
        public string ProficiencyLevel { get; set; }

        /// <summary>
        /// How this skill was added (e.g., "CV", "Manual")
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// ID of the CV upload this skill was extracted from (null if added manually)
        /// </summary>
        public Guid? CvUploadId { get; set; }

        // Navigation properties
        public virtual AppUser User { get; set; }
        public virtual Skill Skill { get; set; }
        public virtual CvUpload CvUpload { get; set; }

        public UserSkill()
        {
        }

        public UserSkill(Guid userId, Guid skillId, string source)
        {
            UserId = userId;
            SkillId = skillId;
            Source = source;
        }
    }
}
