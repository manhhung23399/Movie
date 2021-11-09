
using Movie.Core.Dtos;
using Movie.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Core.Interfaces
{
    public interface IGenresReponsitory
    {
        Task<object> GetGenresAsync(string genreId = "");
        Task DeleteGenresAsync(string genreId);
        Task AddOrUpdateGenresAsync(Genre genre, string genreId = "");
        Task AddOrUpdateGenresAsync(Genre genre);
    }
}
