using System;
using JobInsight.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace JobInsight.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Repository for Location entity
    /// </summary>
    public class LocationRepository : EfCoreRepository<JobInsightDbContext, Location, Guid>, ILocationRepository
    {
        public LocationRepository(IDbContextProvider<JobInsightDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
