using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobInsight.Entities.Dtos;
using Volo.Abp.Application.Services;

namespace JobInsight.Entities.Services
{
    /// <summary>
    /// Service interface for job management
    /// </summary>
    public interface IJobAppService : IApplicationService
    {
        Task<JobDto> GetAsync(Guid id);
        Task<List<JobDto>> GetListAsync();
        Task<JobDto> CreateAsync(CreateUpdateJobDto input);
        Task<JobDto> UpdateAsync(Guid id, CreateUpdateJobDto input);
        Task DeleteAsync(Guid id);
    }
}
