using System;
using Volo.Abp.Application.Dtos;

namespace JobInsight.Entities.Dtos
{
    /// <summary>
    /// DTO for job-benefit relationship
    /// </summary>
    public class CreateUpdateJobBenefitDto
    {
        public Guid JobId { get; set; }
        public Guid BenefitId { get; set; }
        public string CustomValue { get; set; }
    }

    /// <summary>
    /// DTO for reading job-benefit data
    /// </summary>
    public class JobBenefitDto : EntityDto<Guid>
    {
        public Guid JobId { get; set; }
        public Guid BenefitId { get; set; }
        public string BenefitName { get; set; }
        public string CustomValue { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
