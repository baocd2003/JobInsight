using System;
using Volo.Abp.Application.Dtos;

namespace JobInsight.Entities.Dtos
{
    /// <summary>
    /// DTO for creating/updating a company
    /// </summary>
    public class CreateUpdateCompanyDto
    {
        public string Name { get; set; }
        public string NameSlug { get; set; }
        public string ExternalId { get; set; }
        public string SourceWebsite { get; set; }
        public string Industry { get; set; }
        public string CompanySize { get; set; }
        public string Website { get; set; }
        public string LogoUrl { get; set; }
        public Guid? HeadquarterLocationId { get; set; }
        public string Address { get; set; }
        public string LinkedInUrl { get; set; }
        public string FacebookUrl { get; set; }
        public decimal? AverageSalary { get; set; }
        public string Description { get; set; }
        public string WhyJoinUs { get; set; }
        public decimal? Rating { get; set; }
    }

    /// <summary>
    /// DTO for reading company data
    /// </summary>
    public class CompanyDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string NameSlug { get; set; }
        public string ExternalId { get; set; }
        public string SourceWebsite { get; set; }
        public string Industry { get; set; }
        public string CompanySize { get; set; }
        public string Website { get; set; }
        public string LogoUrl { get; set; }
        public Guid? HeadquarterLocationId { get; set; }
        public string Address { get; set; }
        public string LinkedInUrl { get; set; }
        public string FacebookUrl { get; set; }
        public int TotalJobsPosted { get; set; }
        public int ActiveJobsCount { get; set; }
        public decimal? AverageSalary { get; set; }
        public string Description { get; set; }
        public string WhyJoinUs { get; set; }
        public decimal? Rating { get; set; }
        public int ReviewCount { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
