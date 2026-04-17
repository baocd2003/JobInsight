using System;
using Volo.Abp.Domain.Repositories;

namespace JobInsight.Entities
{
    /// <summary>
    /// Repository interface for Benefit entity
    /// </summary>
    public interface IBenefitRepository : IRepository<Benefit, Guid>
    {
    }
}
