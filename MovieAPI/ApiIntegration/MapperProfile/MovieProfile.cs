using AutoMapper;
using Movie.ApiIntegration.MapperProfile.MapperExtensions;
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
                conf => conf.MapFrom(opt => (new Random()).Next(10)));

            //Response API List Movie
            CreateMap<MovieModel, MovieResponse>()
                .ForMember(dto => dto.id,
                conf => conf.MapFrom(opt => opt.Id))
                .ForMember(dto => dto.poster,
                conf => conf.MapFrom(opt => opt.Poster))
                .ForMember(dto => dto.backdrop,
                conf => conf.MapFrom(opt => opt.BackDrop))
                .ForMember(dto => dto.title,
                conf => conf.MapFrom(opt => opt.Title));
            //Response API Movie Detail
            CreateMap<MovieModel, MovieDetailResponnse>();
        }
    }
}
