
using AutoMapper;
using Movie.Core.Constants;
using Movie.Core.Dtos;
using Movie.Core.Entities;
using Movie.Core.Interfaces;
using Movie.Core.Utils;
using Movie.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace Movie.Infrastructure.Reponsitories
{
    public class MovieReponsitory : IMovieReponsitory
    {
        private readonly IFirebaseManager _manager;
        private IMapper _mapper;
        private IFileEvent _file;
        public MovieReponsitory(
            IFirebaseManager manager, 
            IFileEvent file,
            IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
            _file = file;
        }
        public async Task<MovieModel> AddOrUpdateMovieAsync(MovieDto movieDtos, string movieId = "")
        {
            try
            {
                MovieModel movie = new MovieModel();
                string idMovie = string.IsNullOrEmpty(movieId) ? movieDtos.Id : movieId;
                string path = $"{ArgumentEntities.Movie}/{idMovie}";
                if (string.IsNullOrEmpty(movieId))
                {
                    var checkMovieByTitle = await _manager.Database().GetAsync(ArgumentEntities.Movie);
                    if (checkMovieByTitle.Body != "null")
                    {
                        var data = checkMovieByTitle.ResultAs<Dictionary<string, MovieModel>>();
                        var movies = data.Values.ToList();
                        if (movies.FirstOrDefault(x => !string.IsNullOrEmpty(x.Title) ? x.Title.Equals(movieDtos.Title) : false) != null)
                            throw new Exception(Notify.NOTIFY_ISVALID_MOVIE);
                    }
                }

                if (string.IsNullOrEmpty(movieId))
                {
                    movie = _mapper.Map<MovieModel>(movieDtos);
                    
                    movie.Poster = movieDtos.Poster != null ? await _file.UploadStreamAsync(movieDtos.Poster, ArgumentEntities.Movie) : "";
                    movie.BackDrop = movieDtos.BackDrop != null ? await _file.UploadStreamAsync(movieDtos.BackDrop, ArgumentEntities.Movie) : "";

                    if (!string.IsNullOrEmpty(movieDtos.PosterLink)) movie.Poster = movieDtos.PosterLink;
                    if (!string.IsNullOrEmpty(movieDtos.BackDropLink)) movie.BackDrop = movieDtos.BackDropLink;

                    await _manager.Database().SetAsync(path, movie);
                    return movie;
                }
                else
                {
                    var movieUpdated = (await _manager.Database().GetAsync(path)).ResultAs<MovieModel>();
                    if (movieDtos.Poster != null)
                    {
                        movieUpdated.Poster = await _file.UploadStreamAsync(movieDtos.Poster, ArgumentEntities.Movie);
                    }
                    if(movieDtos.BackDrop != null)
                    {
                        movieUpdated.BackDrop = await _file.UploadStreamAsync(movieDtos.BackDrop, ArgumentEntities.Movie);
                    }
                    if (!string.IsNullOrEmpty(movieDtos.PosterLink)) movieUpdated.Poster = movieDtos.PosterLink;
                    if (!string.IsNullOrEmpty(movieDtos.BackDropLink)) movieUpdated.BackDrop = movieDtos.BackDropLink;
                    movieUpdated.Title = movieDtos.Title;
                    movieUpdated.Description = movieDtos.Description;
                    movieUpdated.Sources = new Sources
                    {
                        mPhimMoi = movieDtos.Sources
                    };
                    movieUpdated.Genres = movieDtos.Genres;
                    movieUpdated.Companies = movieDtos.Companies;
                    movieUpdated.Casts = movieDtos.Casts;
                    movieUpdated.DateRelease = movieDtos.DateRelease;
                    await _manager.Database().SetAsync(path, movieUpdated);
                    return movieUpdated;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddOrUpdateMovieAsync(MovieModel movie)
        {
            try
            {
                string path = $"{ArgumentEntities.Movie}/{movie.Id}";
                await _manager.Database().SetAsync(path, movie);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteMovieAsync(string movieId)
        {
            try
            {
                var data = (MovieModel)await this.GetMovieAsync(movieId);
                if (!string.IsNullOrEmpty(data.PosterName)) _manager.Storage().Child(ArgumentEntities.Movie).Child(data.PosterName).DeleteAsync();
                if (!string.IsNullOrEmpty(data.BackDropName)) _manager.Storage().Child(ArgumentEntities.Movie).Child(data.BackDropName).DeleteAsync();
                _manager.Database().DeleteAsync($"{ArgumentEntities.Movie}/{movieId}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> GetMovieAsync(string movieId = "")
        {
            try
            {
                string path = string.IsNullOrEmpty(movieId) ? $"{ArgumentEntities.Movie}" : $"{ArgumentEntities.Movie}/{movieId}";
                var data = await _manager.Database().GetAsync(path);
                if (data.Body == "null")
                {
                    if (!string.IsNullOrEmpty(movieId)) throw new Exception("Not Found");
                    else return new List<MovieModel>();
                }
                if(string.IsNullOrEmpty(movieId))
                {
                    var movies = data.ResultAs<Dictionary<string, MovieModel>>();
                    return movies.Values.ToList();
                }
                var movie = data.ResultAs<MovieModel>();
                return movie;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
