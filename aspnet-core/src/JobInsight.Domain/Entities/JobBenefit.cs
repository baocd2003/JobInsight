using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace JobInsight.Entities
{
    /// <summary>
    /// Junction entity for Job-Benefit many-to-many relationship
    /// </summary>
    public class JobBenefit : CreationAuditedEntity<Guid>
    {
        /// <summary>
        /// Job ID
        /// </summary>
        public Guid JobId { get; set; }

        /// <summary>
        /// Benefit ID
        /// </summary>
        public Guid BenefitId { get; set; }

        /// <summary>
        /// Custom value for this benefit specific to this job (e.g., "20 days" for annual leave)
        /// </summary>
        public string CustomValue { get; set; }

        // Navigation properties
        public virtual Job Job { get; set; }
        public virtual Benefit Benefit { get; set; }

        public JobBenefit()
        {
        }

        public JobBenefit(Guid jobId, Guid benefitId)
        {
            JobId = jobId;
            BenefitId = benefitId;
        }
    }
}
