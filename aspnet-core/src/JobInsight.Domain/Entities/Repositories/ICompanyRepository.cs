using System;
using Volo.Abp.Domain.Repositories;

namespace JobInsight.Entities
{
    /// <summary>
    /// Repository interface for Company entity
    /// </summary>
    public interface ICompanyRepository : IRepository<Company, Guid>
    {
    }
}
