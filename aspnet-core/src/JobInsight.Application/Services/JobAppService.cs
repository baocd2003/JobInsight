using JobInsight.Entities;
using JobInsight.Entities.Dtos;
using JobInsight.Entities.Services;
using JobInsight.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;

namespace JobInsight.Services
{
    /// <summary>
    /// Application service for job management
    /// </summary>
    public class JobAppService : ApplicationService, IJobAppService
    {
        private readonly IJobRepository _repository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IDistributedEventBus _eventBus;

        public JobAppService(
            IJobRepository repository,
            ICompanyRepository companyRepository,
            ILocationRepository locationRepository,
            IDistributedEventBus eventBus)
        {
            _repository = repository;
            _companyRepository = companyRepository;
            _locationRepository = locationRepository;
            _eventBus = eventBus;
        }

        public async Task<JobDto> GetAsync(Guid id)
        {
            var job = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<Job, JobDto>(job);

            if (job.CompanyId != Guid.Empty)
            {
                var company = await _companyRepository.GetAsync(job.CompanyId);
                dto.CompanyName = company.Name;
            }

            if (job.LocationId.HasValue)
            {
                var location = await _locationRepository.GetAsync(job.LocationId.Value);
                dto.LocationName = location.DisplayName;
            }

            return dto;
        }

        public async Task<List<JobDto>> GetListAsync()
        {
            var jobs = await _repository.GetListAsync();
            var dtos = new List<JobDto>();

            foreach (var job in jobs)
            {
                var dto = ObjectMapper.Map<Job, JobDto>(job);

                if (job.CompanyId != Guid.Empty)
                {
                    var company = await _companyRepository.GetAsync(job.CompanyId);
                    dto.CompanyName = company.Name;
                }

                if (job.LocationId.HasValue)
                {
                    var location = await _locationRepository.GetAsync(job.LocationId.Value);
                    dto.LocationName = location.DisplayName;
                }

                dtos.Add(dto);
            }

            return dtos;
        }

        public async Task<JobDto> CreateAsync(CreateUpdateJobDto input)
        {
            var job = ObjectMapper.Map<CreateUpdateJobDto, Job>(input);
            await _repository.InsertAsync(job);
            
            // Publish event to RabbitMQ for testing
            await _eventBus.PublishAsync(new JobCreatedEventData
            {
                JobId = job.Id,
                JobTitle = job.Title,
                CompanyId = job.CompanyId,
                CreatedAt = DateTime.UtcNow
            });
            
            return await GetAsync(job.Id);
        }

        public async Task<JobDto> UpdateAsync(Guid id, CreateUpdateJobDto input)
        {
            var job = await _repository.GetAsync(id);
            ObjectMapper.Map(input, job);
            await _repository.UpdateAsync(job);
            return await GetAsync(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
        public async Task SendTestMessageAsync(TestMessageEto message)
        {
            Logger.LogInformation($"Sending message to RabbitMQ: {message}");

            await _eventBus.PublishAsync(message);

            Logger.LogInformation("Message sent successfully!");
        }
    }
}
