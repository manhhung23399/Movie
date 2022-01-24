using AutoMapper;
using Movie.Core.Dtos;
using Movie.Core.Entities;
using Movie.Core.Resources.Response;

namespace Movie.ApiIntegration.MapperProfile
{
    public class CastProfile : Profile
    {
        public CastProfile()
        {
            CreateMap<CastDto, Cast>()
                .ForMember(dto => dto.FileName,
                conf => conf.MapFrom(opt => opt.Avatar != null ? opt.Avatar.FileName : ""));
            //Mapper cast to cast response or cast detail response
            CreateMap<Cast, CastResponse>();
            CreateMap<Cast, CastDetailResponse>();
        }
    }
}
