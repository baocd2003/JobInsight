using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobInsight.Entities;
using JobInsight.Entities.Dtos;
using JobInsight.Entities.Services;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace JobInsight.Services
{
    /// <summary>
    /// Application service for location management
    /// </summary>
    public class LocationAppService : ApplicationService, ILocationAppService
    {
        private readonly ILocationRepository _repository;

        public LocationAppService(ILocationRepository repository)
        {
            _repository = repository;
        }

        public async Task<LocationDto> GetAsync(Guid id)
        {
            var location = await _repository.GetAsync(id);
            return ObjectMapper.Map<Location, LocationDto>(location);
        }

        public async Task<List<LocationDto>> GetListAsync()
        {
            var locations = await _repository.GetListAsync();
            return ObjectMapper.Map<List<Location>, List<LocationDto>>(locations);
        }

        public async Task<LocationDto> CreateAsync(CreateUpdateLocationDto input)
        {
            var location = ObjectMapper.Map<CreateUpdateLocationDto, Location>(input);
            await _repository.InsertAsync(location);
            return ObjectMapper.Map<Location, LocationDto>(location);
        }

        public async Task<LocationDto> UpdateAsync(Guid id, CreateUpdateLocationDto input)
        {
            var location = await _repository.GetAsync(id);
            ObjectMapper.Map(input, location);
            await _repository.UpdateAsync(location);
            return ObjectMapper.Map<Location, LocationDto>(location);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
