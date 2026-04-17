using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace JobInsight.Entities
{
    /// <summary>
    /// Tracks user-saved jobs
    /// </summary>
    public class UserSavedJob : CreationAuditedEntity<Guid>
    {
        /// <summary>
        /// User ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Job ID that was saved
        /// </summary>
        public Guid JobId { get; set; }

        /// <summary>
        /// User notes about this saved job
        /// </summary>
        public string Notes { get; set; }

        // Navigation properties
        public virtual Job Job { get; set; }

        public UserSavedJob()
        {
        }

        public UserSavedJob(Guid userId, Guid jobId)
        {
            UserId = userId;
            JobId = jobId;
        }
    }
}
