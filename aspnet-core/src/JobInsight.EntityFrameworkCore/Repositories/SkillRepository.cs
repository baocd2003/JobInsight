using System;
using JobInsight.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace JobInsight.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Repository for Skill entity
    /// </summary>
    public class SkillRepository : EfCoreRepository<JobInsightDbContext, Skill, Guid>, ISkillRepository
    {
        public SkillRepository(IDbContextProvider<JobInsightDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
