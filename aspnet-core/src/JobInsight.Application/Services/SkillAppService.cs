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
    /// Application service for skill management
    /// </summary>
    public class SkillAppService : ApplicationService, ISkillAppService
    {
        private readonly ISkillRepository _repository;

        public SkillAppService(ISkillRepository repository)
        {
            _repository = repository;
        }

        public async Task<SkillDto> GetAsync(Guid id)
        {
            var skill = await _repository.GetAsync(id);
            return ObjectMapper.Map<Skill, SkillDto>(skill);
        }

        public async Task<List<SkillDto>> GetListAsync()
        {
            var skills = await _repository.GetListAsync();
            return ObjectMapper.Map<List<Skill>, List<SkillDto>>(skills);
        }

        public async Task<SkillDto> CreateAsync(CreateUpdateSkillDto input)
        {
            var skill = ObjectMapper.Map<CreateUpdateSkillDto, Skill>(input);
            await _repository.InsertAsync(skill);
            return ObjectMapper.Map<Skill, SkillDto>(skill);
        }

        public async Task<SkillDto> UpdateAsync(Guid id, CreateUpdateSkillDto input)
        {
            var skill = await _repository.GetAsync(id);
            ObjectMapper.Map(input, skill);
            await _repository.UpdateAsync(skill);
            return ObjectMapper.Map<Skill, SkillDto>(skill);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
