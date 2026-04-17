using AutoMapper;
using JobInsight.Entities;
using JobInsight.Entities.Dtos;

namespace JobInsight;

public class JobInsightApplicationAutoMapperProfile : Profile
{
    public JobInsightApplicationAutoMapperProfile()
    {
        // Job
        CreateMap<Job, JobDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company != null ? src.Company.Name : null))
            .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location != null ? src.Location.DisplayName : null));
        CreateMap<CreateUpdateJobDto, Job>();

        // Company
        CreateMap<Company, CompanyDto>();
        CreateMap<CreateUpdateCompanyDto, Company>();

        // Skill
        CreateMap<Skill, SkillDto>();
        CreateMap<CreateUpdateSkillDto, Skill>();

        // JobSkill
        CreateMap<JobSkill, JobSkillDto>()
            .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.Skill != null ? src.Skill.Name : null));
        CreateMap<CreateUpdateJobSkillDto, JobSkill>();

        // Location
        CreateMap<Location, LocationDto>();
        CreateMap<CreateUpdateLocationDto, Location>();

        // Benefit
        CreateMap<Benefit, BenefitDto>();
        CreateMap<CreateUpdateBenefitDto, Benefit>();

        // JobBenefit
        CreateMap<JobBenefit, JobBenefitDto>()
            .ForMember(dest => dest.BenefitName, opt => opt.MapFrom(src => src.Benefit != null ? src.Benefit.Name : null));
        CreateMap<CreateUpdateJobBenefitDto, JobBenefit>();
    }
}
