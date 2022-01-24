
using Movie.Core.Dtos;
using Movie.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Core.Interfaces
{
    public interface ICastReponsitory
    {
        Task<object> GetCastAsync(string castId = "");
        Task DeleteCastAsync(string castId);
        Task AddOrUpdateCastAsync(Cast cast);
        Task<Cast> AddOrUpdateCastAsync(CastDto castDto, string castId = "");
    }
}
