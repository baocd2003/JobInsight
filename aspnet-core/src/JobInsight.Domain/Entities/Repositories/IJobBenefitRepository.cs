using System;
using Volo.Abp.Domain.Repositories;

namespace JobInsight.Entities
{
    /// <summary>
    /// Repository interface for JobBenefit entity
    /// </summary>
    public interface IJobBenefitRepository : IRepository<JobBenefit, Guid>
    {
    }
}
