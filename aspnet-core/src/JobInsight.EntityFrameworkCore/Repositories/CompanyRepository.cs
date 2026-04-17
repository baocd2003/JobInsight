using System;
using JobInsight.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace JobInsight.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Repository for Company entity
    /// </summary>
    public class CompanyRepository : EfCoreRepository<JobInsightDbContext, Company, Guid>, ICompanyRepository
    {
        public CompanyRepository(IDbContextProvider<JobInsightDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
