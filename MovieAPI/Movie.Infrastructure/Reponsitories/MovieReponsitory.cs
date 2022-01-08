
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
        public async Task AddOrUpdateMovieAsync(MovieDto movieDtos, string movieId = "")
        {
            try
            {
                MovieModel movie = new MovieModel();
                string idMovie = string.IsNullOrEmpty(movieId) ? movie.Id : movieId;
                string path = $"{ArgumentEntities.Movie}/{idMovie}";
                if (string.IsNullOrEmpty(movieId))
                {
                    var checkMovieByTitle = await _manager.Database().GetAsync(ArgumentEntities.Movie);
                    if (checkMovieByTitle.Body != "null")
                    {
                        var data = checkMovieByTitle.ResultAs<Dictionary<string, MovieModel>>();
                        if (data.Values.ToList().FirstOrDefault(x => x.Title.Equals(movieDtos.Title)) != null)
                            throw new Exception(Notify.NOTIFY_ISVALID_MOVIE);
                    }
                }

                if (movieDtos.Poster != null) movie.Poster = await _file.UploadStreamAsync(movieDtos.Poster, ArgumentEntities.Movie);
                if (movieDtos.BackDrop != null) movie.BackDrop = await _file.UploadStreamAsync(movieDtos.BackDrop, ArgumentEntities.Movie);
                if (string.IsNullOrEmpty(movieId))
                {
                    movie = _mapper.Map<MovieModel>(movieDtos);
                    await _manager.Database().SetAsync(path, movie);
                }
                else
                {
                    var updatedMovie = movieDtos.ReClass();
                    await _manager.Database().UpdateAsync(path, updatedMovie);
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
                var task1 = _manager.Storage().Child(ArgumentEntities.Movie).Child(data.PosterName).DeleteAsync();
                var task2 = _manager.Storage().Child(ArgumentEntities.Movie).Child(data.BackDropName).DeleteAsync();
                var task3 = _manager.Database().DeleteAsync($"{ArgumentEntities.Movie}/{movieId}");
                Task.WaitAll(task1, task2, task3);
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
