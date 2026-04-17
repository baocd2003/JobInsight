using System;
using Volo.Abp.Domain.Repositories;

namespace JobInsight.Entities
{
    /// <summary>
    /// Repository interface for Job entity
    /// </summary>
    public interface IJobRepository : IRepository<Job, Guid>
    {
    }
}
