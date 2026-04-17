using System;
using Volo.Abp.Domain.Repositories;

namespace JobInsight.Entities
{
    /// <summary>
    /// Repository interface for Location entity
    /// </summary>
    public interface ILocationRepository : IRepository<Location, Guid>
    {
    }
}
