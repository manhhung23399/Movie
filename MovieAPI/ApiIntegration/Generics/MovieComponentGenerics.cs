using Microsoft.Extensions.Caching.Memory;
using Movie.ApiIntegration.Cache;
using Movie.Core.Entities;
using Movie.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Infrastructure.Extensions.Generics
{
    public class MovieComponentGenerics<T>  where T : EntityBase
    {
        private IMemoryCache _memoryCacheEntry;
        private ISetCacheMemory _memoryCache;

        public MovieComponentGenerics(ISetCacheMemory memoryCache, IMemoryCache memoryCacheEntry)
        {
            _memoryCache = memoryCache;
            _memoryCacheEntry = memoryCacheEntry;
        }

        public async Task<List<T>> ConvertListAsync(List<string> componentIds)
        {
            try
            {
                var genericList = new List<T>();
                var result = new List<T>();

                genericList = (List<T>)await _memoryCache.SetCacheAsync(genericList, 365, _memoryCacheEntry);
                foreach (var componentId in componentIds)
                {
                    string idReplaced = componentId.Replace(" ", "");
                    try
                    {
                        var item = genericList.FirstOrDefault(x => x.Id.Equals(idReplaced));
                        if (item != null) result.Add(item);
                    }
                    catch { }
                }
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
