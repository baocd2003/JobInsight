using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobInsight.Entities.Dtos;
using Volo.Abp.Application.Services;

namespace JobInsight.Entities.Services
{
    /// <summary>
    /// Service interface for company management
    /// </summary>
    public interface ICompanyAppService : IApplicationService
    {
        Task<CompanyDto> GetAsync(Guid id);
        Task<List<CompanyDto>> GetListAsync();
        Task<CompanyDto> CreateAsync(CreateUpdateCompanyDto input);
        Task<CompanyDto> UpdateAsync(Guid id, CreateUpdateCompanyDto input);
        Task DeleteAsync(Guid id);
    }
}
