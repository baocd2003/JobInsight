using System;
using JobInsight.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace JobInsight.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Repository for JobBenefit entity
    /// </summary>
    public class JobBenefitRepository : EfCoreRepository<JobInsightDbContext, JobBenefit, Guid>, IJobBenefitRepository
    {
        public JobBenefitRepository(IDbContextProvider<JobInsightDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
