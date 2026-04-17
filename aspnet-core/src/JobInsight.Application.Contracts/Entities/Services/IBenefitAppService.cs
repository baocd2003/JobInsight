using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobInsight.Entities.Dtos;
using Volo.Abp.Application.Services;

namespace JobInsight.Entities.Services
{
    /// <summary>
    /// Service interface for benefit management
    /// </summary>
    public interface IBenefitAppService : IApplicationService
    {
        Task<BenefitDto> GetAsync(Guid id);
        Task<List<BenefitDto>> GetListAsync();
        Task<BenefitDto> CreateAsync(CreateUpdateBenefitDto input);
        Task<BenefitDto> UpdateAsync(Guid id, CreateUpdateBenefitDto input);
        Task DeleteAsync(Guid id);
    }
}
