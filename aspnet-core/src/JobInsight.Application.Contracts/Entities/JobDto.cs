using System;
using Volo.Abp.Application.Dtos;

namespace JobInsight.Entities.Dtos
{
    /// <summary>
    /// DTO for creating/updating a job
    /// </summary>
    public class CreateUpdateJobDto
    {
        public string Title { get; set; }
        public Guid CompanyId { get; set; }
        public string ExternalId { get; set; }
        public string SourceWebsite { get; set; }
        public string SourceUrl { get; set; }
        public Guid? LocationId { get; set; }
        public string WorkMode { get; set; }
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }
        public string Currency { get; set; } = "USD";
        public string SalaryDisplay { get; set; }
        public bool IsNegotiable { get; set; }
        public string ExperienceLevel { get; set; }
        public int? YearsOfExperienceMin { get; set; }
        public int? YearsOfExperienceMax { get; set; }
        public string JobType { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public string Benefits { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string Tags { get; set; }
    }

    /// <summary>
    /// DTO for reading job data
    /// </summary>
    public class JobDto : EntityDto<Guid>
    {
        public string Title { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string ExternalId { get; set; }
        public string SourceWebsite { get; set; }
        public string SourceUrl { get; set; }
        public Guid? LocationId { get; set; }
        public string LocationName { get; set; }
        public string WorkMode { get; set; }
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }
        public string Currency { get; set; }
        public string SalaryDisplay { get; set; }
        public bool IsNegotiable { get; set; }
        public string ExperienceLevel { get; set; }
        public int? YearsOfExperienceMin { get; set; }
        public int? YearsOfExperienceMax { get; set; }
        public string JobType { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public string Benefits { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? LastCrawledAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsExpired { get; set; }
        public int ViewCount { get; set; }
        public string Tags { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
