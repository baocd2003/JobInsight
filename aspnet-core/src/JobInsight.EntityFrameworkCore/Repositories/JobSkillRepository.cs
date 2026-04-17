using System;
using JobInsight.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace JobInsight.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Repository for JobSkill entity
    /// </summary>
    public class JobSkillRepository : EfCoreRepository<JobInsightDbContext, JobSkill, Guid>, IJobSkillRepository
    {
        public JobSkillRepository(IDbContextProvider<JobInsightDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
