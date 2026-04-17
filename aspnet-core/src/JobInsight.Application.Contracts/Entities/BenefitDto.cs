using System;
using Volo.Abp.Application.Dtos;

namespace JobInsight.Entities.Dtos
{
    /// <summary>
    /// DTO for creating/updating a benefit
    /// </summary>
    public class CreateUpdateBenefitDto
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Category { get; set; }
        public string IconName { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// DTO for reading benefit data
    /// </summary>
    public class BenefitDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Category { get; set; }
        public string IconName { get; set; }
        public string Description { get; set; }
        public int TotalCompaniesOffering { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
