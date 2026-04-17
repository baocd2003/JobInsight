using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobInsight.Entities.Dtos;
using Volo.Abp.Application.Services;

namespace JobInsight.Entities.Services
{
    /// <summary>
    /// Service interface for job-benefit management
    /// </summary>
    public interface IJobBenefitAppService : IApplicationService
    {
        Task<JobBenefitDto> GetAsync(Guid id);
        Task<List<JobBenefitDto>> GetListByJobAsync(Guid jobId);
        Task<JobBenefitDto> CreateAsync(CreateUpdateJobBenefitDto input);
        Task<JobBenefitDto> UpdateAsync(Guid id, CreateUpdateJobBenefitDto input);
        Task DeleteAsync(Guid id);
    }
}
