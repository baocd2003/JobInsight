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
    /// Application service for job-benefit management
    /// </summary>
    public class JobBenefitAppService : ApplicationService, IJobBenefitAppService
    {
        private readonly IJobBenefitRepository _repository;
        private readonly IBenefitRepository _benefitRepository;

        public JobBenefitAppService(
            IJobBenefitRepository repository,
            IBenefitRepository benefitRepository)
        {
            _repository = repository;
            _benefitRepository = benefitRepository;
        }

        public async Task<JobBenefitDto> GetAsync(Guid id)
        {
            var jobBenefit = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<JobBenefit, JobBenefitDto>(jobBenefit);

            var benefit = await _benefitRepository.GetAsync(jobBenefit.BenefitId);
            dto.BenefitName = benefit.Name;

            return dto;
        }

        public async Task<List<JobBenefitDto>> GetListByJobAsync(Guid jobId)
        {
            var jobBenefits = await _repository.GetListAsync(x => x.JobId == jobId);
            var dtos = new List<JobBenefitDto>();

            foreach (var jobBenefit in jobBenefits)
            {
                var dto = ObjectMapper.Map<JobBenefit, JobBenefitDto>(jobBenefit);
                var benefit = await _benefitRepository.GetAsync(jobBenefit.BenefitId);
                dto.BenefitName = benefit.Name;
                dtos.Add(dto);
            }

            return dtos;
        }

        public async Task<JobBenefitDto> CreateAsync(CreateUpdateJobBenefitDto input)
        {
            var jobBenefit = ObjectMapper.Map<CreateUpdateJobBenefitDto, JobBenefit>(input);
            await _repository.InsertAsync(jobBenefit);
            return await GetAsync(jobBenefit.Id);
        }

        public async Task<JobBenefitDto> UpdateAsync(Guid id, CreateUpdateJobBenefitDto input)
        {
            var jobBenefit = await _repository.GetAsync(id);
            ObjectMapper.Map(input, jobBenefit);
            await _repository.UpdateAsync(jobBenefit);
            return await GetAsync(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
