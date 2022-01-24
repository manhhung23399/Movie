using AutoMapper;
using Movie.ApiIntegration.MapperProfile.MapperExtensions;
using Movie.ApiIntegration.ServerContainer;
using Movie.Core.Dtos;
using Movie.Core.Entities;
using Movie.Core.Resources.Response;
using Movie.Core.Utils;
using Movie.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

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
                conf => conf.MapFrom(opt => RandomNumber.GetRandomNumber(3.11, 9.99)))
                .ForMember(dto => dto.Sources,
                conf => conf.MapFrom(opt => new Sources { mPhimMoi = opt.Sources ?? ""  }));

            CreateMap<MovieModel, MovieResponse>();
            CreateMap<MovieModel, MovieDetailResponseAD>()
                .ForMember(dto => dto.Genres,
                conf => conf.MapFrom(opt => opt.Genres.SplitStringToList(",")))
                .ForMember(dto => dto.Companies,
                conf => conf.MapFrom(opt => opt.Companies.SplitStringToList(",")))
                .ForMember(dto => dto.Cats,
                conf => conf.MapFrom(opt => opt.Casts.SplitStringToList(",")))
                .ForMember(dto => dto.Source,
                conf => conf.MapFrom(opt => opt.Sources.mPhimMoi));

            CreateMap<MovieModel, MovieDetailResponnse>()
                .ForMember(dto => dto.Sources,
                conf => conf.MapFrom(opt => opt.Sources.GetLink()));
        }
    }
}

