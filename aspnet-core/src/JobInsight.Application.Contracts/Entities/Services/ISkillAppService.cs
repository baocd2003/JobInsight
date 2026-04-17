using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobInsight.Entities.Dtos;
using Volo.Abp.Application.Services;

namespace JobInsight.Entities.Services
{
    /// <summary>
    /// Service interface for skill management
    /// </summary>
    public interface ISkillAppService : IApplicationService
    {
        Task<SkillDto> GetAsync(Guid id);
        Task<List<SkillDto>> GetListAsync();
        Task<SkillDto> CreateAsync(CreateUpdateSkillDto input);
        Task<SkillDto> UpdateAsync(Guid id, CreateUpdateSkillDto input);
        Task DeleteAsync(Guid id);
    }
}
