using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace JobInsight.Entities
{
    /// <summary>
    /// Represents job alert subscription for a user
    /// </summary>
    public class UserJobAlert : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// User ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Keywords to search for in job titles/descriptions
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// JSON array of Skill IDs to filter by
        /// </summary>
        public string SkillIds { get; set; }

        /// <summary>
        /// JSON array of Location IDs to filter by
        /// </summary>
        public string LocationIds { get; set; }

        /// <summary>
        /// Minimum salary threshold
        /// </summary>
        public decimal? MinSalary { get; set; }

        /// <summary>
        /// Required experience level (e.g., "Junior", "Mid", "Senior")
        /// </summary>
        public string ExperienceLevel { get; set; }

        /// <summary>
        /// Whether this alert is active
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Alert frequency (e.g., "Daily", "Weekly", "Instant")
        /// </summary>
        public string Frequency { get; set; } = "Weekly";

        /// <summary>
        /// Last time alert was sent
        /// </summary>
        public DateTime? LastSentAt { get; set; }

        public UserJobAlert()
        {
        }

        public UserJobAlert(Guid userId)
        {
            UserId = userId;
            IsActive = true;
            Frequency = "Weekly";
        }
    }
}
