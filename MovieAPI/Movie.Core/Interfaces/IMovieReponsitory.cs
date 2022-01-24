
using Movie.Core.Dtos;
using Movie.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Core.Interfaces
{
    public interface IMovieReponsitory
    {
        Task<object> GetMovieAsync(string movieId = "");
        Task DeleteMovieAsync(string movieId);
        Task AddOrUpdateMovieAsync(MovieModel movieDtos);
        Task<MovieModel> AddOrUpdateMovieAsync(MovieDto movieDtos, string movieId = "");
    }
}
