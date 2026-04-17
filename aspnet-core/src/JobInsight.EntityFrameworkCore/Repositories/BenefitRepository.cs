using System;
using JobInsight.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace JobInsight.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Repository for Benefit entity
    /// </summary>
    public class BenefitRepository : EfCoreRepository<JobInsightDbContext, Benefit, Guid>, IBenefitRepository
    {
        public BenefitRepository(IDbContextProvider<JobInsightDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
