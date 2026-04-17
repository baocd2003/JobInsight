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
    /// Application service for job-skill management
    /// </summary>
    public class JobSkillAppService : ApplicationService, IJobSkillAppService
    {
        private readonly IJobSkillRepository _repository;
        private readonly ISkillRepository _skillRepository;

        public JobSkillAppService(
            IJobSkillRepository repository,
            ISkillRepository skillRepository)
        {
            _repository = repository;
            _skillRepository = skillRepository;
        }

        public async Task<JobSkillDto> GetAsync(Guid id)
        {
            var jobSkill = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<JobSkill, JobSkillDto>(jobSkill);

            var skill = await _skillRepository.GetAsync(jobSkill.SkillId);
            dto.SkillName = skill.Name;

            return dto;
        }

        public async Task<List<JobSkillDto>> GetListByJobAsync(Guid jobId)
        {
            var jobSkills = await _repository.GetListAsync(x => x.JobId == jobId);
            var dtos = new List<JobSkillDto>();

            foreach (var jobSkill in jobSkills)
            {
                var dto = ObjectMapper.Map<JobSkill, JobSkillDto>(jobSkill);
                var skill = await _skillRepository.GetAsync(jobSkill.SkillId);
                dto.SkillName = skill.Name;
                dtos.Add(dto);
            }

            return dtos;
        }

        public async Task<JobSkillDto> CreateAsync(CreateUpdateJobSkillDto input)
        {
            var jobSkill = ObjectMapper.Map<CreateUpdateJobSkillDto, JobSkill>(input);
            await _repository.InsertAsync(jobSkill);
            return await GetAsync(jobSkill.Id);
        }

        public async Task<JobSkillDto> UpdateAsync(Guid id, CreateUpdateJobSkillDto input)
        {
            var jobSkill = await _repository.GetAsync(id);
            ObjectMapper.Map(input, jobSkill);
            await _repository.UpdateAsync(jobSkill);
            return await GetAsync(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
