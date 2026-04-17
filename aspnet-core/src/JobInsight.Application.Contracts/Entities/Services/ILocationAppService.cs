using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobInsight.Entities.Dtos;
using Volo.Abp.Application.Services;

namespace JobInsight.Entities.Services
{
    /// <summary>
    /// Service interface for location management
    /// </summary>
    public interface ILocationAppService : IApplicationService
    {
        Task<LocationDto> GetAsync(Guid id);
        Task<List<LocationDto>> GetListAsync();
        Task<LocationDto> CreateAsync(CreateUpdateLocationDto input);
        Task<LocationDto> UpdateAsync(Guid id, CreateUpdateLocationDto input);
        Task DeleteAsync(Guid id);
    }
}
