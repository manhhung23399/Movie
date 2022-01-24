using AutoMapper;
using Movie.Core.Dtos;
using Movie.Core.Entities;

namespace Movie.ApiIntegration.MapperProfile
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<CompanyDto, Company>()
                .ForMember(dto => dto.FileName,
                conf => conf.MapFrom(opt => opt.Logo != null ? opt.Logo.FileName : ""));
        }
    }
}
