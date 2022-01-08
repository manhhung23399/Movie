using AutoMapper;
using Movie.ApiIntegration.MapperProfile.MapperExtensions;
using Movie.ApiIntegration.ServerContainer;
using Movie.Core.Dtos;
using Movie.Core.Entities;
using Movie.Core.Resources.Response;
using Movie.Infrastructure.Extensions;
using System;

namespace Movie.ApiIntegration.MapperProfile
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            //Data tranfer objecto to entity movie
            CreateMap<MovieDto, MovieModel>()
                .ForMember(dto => dto.PosterName,
                conf => conf.MapFrom(opt => opt.Poster != null ? opt.Poster.FileName : ""))
                .ForMember(dto => dto.BackDropName,
                conf => conf.MapFrom(opt => opt.BackDrop != null ? opt.BackDrop.FileName : ""))
                .ForMember(dto => dto.Score,
                conf => conf.MapFrom(opt => (new Random()).Next(10)))
                .ForMember(dto => dto.Sources,
                conf => conf.MapFrom(opt => new Sources { mPhimMoi = opt.Sources.mPhimMoi ?? ""  }));

            CreateMap<MovieModel, MovieResponse>();
            CreateMap<MovieModel, MovieDetailResponnse>()
                .ForMember(dto => dto.Sources,
                conf => conf.MapFrom(opt => opt.Sources.GetLink()));
        }
    }
}
