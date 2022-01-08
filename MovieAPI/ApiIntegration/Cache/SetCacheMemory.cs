using Microsoft.Extensions.Caching.Memory;
using Movie.Core.Constants;
using Movie.Core.Entities;
using Movie.Core.Interfaces;
using Movie.Core.Resources.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.ApiIntegration.Cache
{
    public class SetCacheMemory : ISetCacheMemory
    {
        private IUnitOfWork _unitOfWork;
        public SetCacheMemory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<object> SetCacheAsync(object input, int days, IMemoryCache _memoryCache)
        {
            string stateCache = "";
            if (input is List<MovieModel>) stateCache = CacheEntryKeys.MOVIE_CACHE;
            if (input is List<Genre>) stateCache = CacheEntryKeys.GENRE_CACHE;
            if (input is List<Cast> || input is List<CastResponse>) stateCache = CacheEntryKeys.CAST_CACHE;
            if (input is List<Company>) stateCache = CacheEntryKeys.COMPANY_CACHE;
            if (!string.IsNullOrEmpty(stateCache) && !_memoryCache.TryGetValue(stateCache, out input))
            {
                if (stateCache == CacheEntryKeys.MOVIE_CACHE) input = (List<MovieModel>)await _unitOfWork.Movie.GetMovieAsync();
                if (stateCache == CacheEntryKeys.GENRE_CACHE) input = (List<Genre>)await _unitOfWork.Genre.GetGenresAsync();
                if (stateCache == CacheEntryKeys.CAST_CACHE) input = (List<Cast>)await _unitOfWork.Cast.GetCastAsync();
                if (stateCache == CacheEntryKeys.COMPANY_CACHE) input = (List<Company>)await _unitOfWork.Company.GetCompanyAsync();
                if(days > 0)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(days));
                    _memoryCache.Set(stateCache, input, cacheEntryOptions);
                }
            }
            return input;
        }
    }
}
