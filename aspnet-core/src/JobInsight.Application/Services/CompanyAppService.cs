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
    /// Application service for company management
    /// </summary>
    public class CompanyAppService : ApplicationService, ICompanyAppService
    {
        private readonly ICompanyRepository _repository;

        public CompanyAppService(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<CompanyDto> GetAsync(Guid id)
        {
            var company = await _repository.GetAsync(id);
            return ObjectMapper.Map<Company, CompanyDto>(company);
        }

        public async Task<List<CompanyDto>> GetListAsync()
        {
            var companies = await _repository.GetListAsync();
            return ObjectMapper.Map<List<Company>, List<CompanyDto>>(companies);
        }

        public async Task<CompanyDto> CreateAsync(CreateUpdateCompanyDto input)
        {
            var company = ObjectMapper.Map<CreateUpdateCompanyDto, Company>(input);
            await _repository.InsertAsync(company);
            return ObjectMapper.Map<Company, CompanyDto>(company);
        }

        public async Task<CompanyDto> UpdateAsync(Guid id, CreateUpdateCompanyDto input)
        {
            var company = await _repository.GetAsync(id);
            ObjectMapper.Map(input, company);
            await _repository.UpdateAsync(company);
            return ObjectMapper.Map<Company, CompanyDto>(company);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
