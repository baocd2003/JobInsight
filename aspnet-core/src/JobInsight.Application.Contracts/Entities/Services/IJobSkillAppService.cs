using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobInsight.Entities.Dtos;
using Volo.Abp.Application.Services;

namespace JobInsight.Entities.Services
{
    /// <summary>
    /// Service interface for job-skill management
    /// </summary>
    public interface IJobSkillAppService : IApplicationService
    {
        Task<JobSkillDto> GetAsync(Guid id);
        Task<List<JobSkillDto>> GetListByJobAsync(Guid jobId);
        Task<JobSkillDto> CreateAsync(CreateUpdateJobSkillDto input);
        Task<JobSkillDto> UpdateAsync(Guid id, CreateUpdateJobSkillDto input);
        Task DeleteAsync(Guid id);
    }
}
