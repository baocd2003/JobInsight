using System;
using Volo.Abp.Domain.Repositories;

namespace JobInsight.Entities
{
    /// <summary>
    /// Repository interface for JobSkill entity
    /// </summary>
    public interface IJobSkillRepository : IRepository<JobSkill, Guid>
    {
    }
}
