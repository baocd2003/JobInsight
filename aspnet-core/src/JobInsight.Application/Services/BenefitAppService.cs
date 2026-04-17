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
    /// Application service for benefit management
    /// </summary>
    public class BenefitAppService : ApplicationService, IBenefitAppService
    {
        private readonly IBenefitRepository _repository;

        public BenefitAppService(IBenefitRepository repository)
        {
            _repository = repository;
        }

        public async Task<BenefitDto> GetAsync(Guid id)
        {
            var benefit = await _repository.GetAsync(id);
            return ObjectMapper.Map<Benefit, BenefitDto>(benefit);
        }

        public async Task<List<BenefitDto>> GetListAsync()
        {
            var benefits = await _repository.GetListAsync();
            return ObjectMapper.Map<List<Benefit>, List<BenefitDto>>(benefits);
        }

        public async Task<BenefitDto> CreateAsync(CreateUpdateBenefitDto input)
        {
            var benefit = ObjectMapper.Map<CreateUpdateBenefitDto, Benefit>(input);
            await _repository.InsertAsync(benefit);
            return ObjectMapper.Map<Benefit, BenefitDto>(benefit);
        }

        public async Task<BenefitDto> UpdateAsync(Guid id, CreateUpdateBenefitDto input)
        {
            var benefit = await _repository.GetAsync(id);
            ObjectMapper.Map(input, benefit);
            await _repository.UpdateAsync(benefit);
            return ObjectMapper.Map<Benefit, BenefitDto>(benefit);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
