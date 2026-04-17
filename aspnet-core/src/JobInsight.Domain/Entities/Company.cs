using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace JobInsight.Entities
{
    /// <summary>
    /// Represents a company posting jobs
    /// </summary>
    public class Company : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// Company name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL-friendly slug for company name
        /// </summary>
        public string NameSlug { get; set; }

        /// <summary>
        /// External ID from source website
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// Source website (e.g., "ITviec", "TopCV", "VietnamWorks")
        /// </summary>
        public string SourceWebsite { get; set; }

        /// <summary>
        /// Industry sector
        /// </summary>
        public string Industry { get; set; }

        /// <summary>
        /// Company size bracket (e.g., "1-50", "51-200", "201-500", "500+")
        /// </summary>
        public string CompanySize { get; set; }

        /// <summary>
        /// Company website URL
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Logo URL
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// Headquarters location ID
        /// </summary>
        public Guid? HeadquarterLocationId { get; set; }

        /// <summary>
        /// Full address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// LinkedIn company URL
        /// </summary>
        public string LinkedInUrl { get; set; }

        /// <summary>
        /// Facebook company URL
        /// </summary>
        public string FacebookUrl { get; set; }

        /// <summary>
        /// Total jobs posted by this company (all time)
        /// </summary>
        public int TotalJobsPosted { get; set; }

        /// <summary>
        /// Count of currently active jobs
        /// </summary>
        public int ActiveJobsCount { get; set; }

        /// <summary>
        /// Average salary offered
        /// </summary>
        public decimal? AverageSalary { get; set; }

        /// <summary>
        /// Company description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Why join us statement/description
        /// </summary>
        public string WhyJoinUs { get; set; }

        /// <summary>
        /// Company rating (0-5)
        /// </summary>
        public decimal? Rating { get; set; }

        /// <summary>
        /// Number of reviews
        /// </summary>
        public int ReviewCount { get; set; }

        public Company()
        {
        }

        public Company(string name, string nameSlug)
        {
            Name = name;
            NameSlug = nameSlug;
        }
    }
}
