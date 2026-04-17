using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace JobInsight.Entities
{
    /// <summary>
    /// Represents a job posting
    /// </summary>
    public class Job : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// Job title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Company ID that posted this job
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// External ID from source website
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// Source website (e.g., "ITviec", "TopCV", "VietnamWorks")
        /// </summary>
        public string SourceWebsite { get; set; }

        /// <summary>
        /// Source URL to the original job posting
        /// </summary>
        public string SourceUrl { get; set; }

        /// <summary>
        /// Location ID for this job
        /// </summary>
        public Guid? LocationId { get; set; }

        /// <summary>
        /// Work mode (e.g., "Remote", "Hybrid", "Onsite")
        /// </summary>
        public string WorkMode { get; set; }

        /// <summary>
        /// Minimum salary
        /// </summary>
        public decimal? SalaryMin { get; set; }

        /// <summary>
        /// Maximum salary
        /// </summary>
        public decimal? SalaryMax { get; set; }

        /// <summary>
        /// Currency code (e.g., "USD", "VND")
        /// </summary>
        public string Currency { get; set; } = "USD";

        /// <summary>
        /// Salary display text as shown on the posting
        /// </summary>
        public string SalaryDisplay { get; set; }

        /// <summary>
        /// Flag indicating if salary is negotiable
        /// </summary>
        public bool IsNegotiable { get; set; }

        /// <summary>
        /// Required experience level (e.g., "Intern", "Junior", "Mid", "Senior", "Lead", "Manager")
        /// </summary>
        public string ExperienceLevel { get; set; }

        /// <summary>
        /// Minimum years of experience required
        /// </summary>
        public int? YearsOfExperienceMin { get; set; }

        /// <summary>
        /// Maximum years of experience
        /// </summary>
        public int? YearsOfExperienceMax { get; set; }

        /// <summary>
        /// Job type (e.g., "Full-time", "Part-time", "Contract", "Freelance")
        /// </summary>
        public string JobType { get; set; }

        /// <summary>
        /// Job description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Job requirements
        /// </summary>
        public string Requirements { get; set; }

        /// <summary>
        /// Job benefits/perks
        /// </summary>
        public string Benefits { get; set; }

        /// <summary>
        /// Date when job was posted
        /// </summary>
        public DateTime PostedDate { get; set; }

        /// <summary>
        /// Expiration date of the job posting
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Last time this job was crawled/updated
        /// </summary>
        public DateTime? LastCrawledAt { get; set; }

        /// <summary>
        /// Whether this job is currently active
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Whether this job has expired
        /// </summary>
        public bool IsExpired { get; set; }

        /// <summary>
        /// Number of views this job received
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// JSON array of tags for this job
        /// </summary>
        public string Tags { get; set; }

        // Navigation properties
        public virtual Company Company { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<JobSkill> JobSkills { get; set; }
        public virtual ICollection<JobBenefit> JobBenefits { get; set; }
        public virtual ICollection<UserSavedJob> SavedByUsers { get; set; }

        public Job()
        {
            JobSkills = new List<JobSkill>();
            JobBenefits = new List<JobBenefit>();
            SavedByUsers = new List<UserSavedJob>();
        }

        public Job(string title, Guid companyId, string sourceWebsite)
        {
            Title = title;
            CompanyId = companyId;
            SourceWebsite = sourceWebsite;
            PostedDate = DateTime.UtcNow;
            IsActive = true;
            JobSkills = new List<JobSkill>();
            JobBenefits = new List<JobBenefit>();
            SavedByUsers = new List<UserSavedJob>();
        }
    }
}
