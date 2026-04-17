using System;
using Volo.Abp.Domain.Repositories;

namespace JobInsight.Entities
{
    /// <summary>
    /// Repository interface for Skill entity
    /// </summary>
    public interface ISkillRepository : IRepository<Skill, Guid>
    {
    }
}
