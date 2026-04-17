using System;
using JobInsight.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace JobInsight.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Repository for Job entity
    /// </summary>
    public class JobRepository : EfCoreRepository<JobInsightDbContext, Job, Guid>, IJobRepository
    {
        public JobRepository(IDbContextProvider<JobInsightDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
