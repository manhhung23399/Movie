using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace Movie.ApiIntegration.Cache
{
    public interface ISetCacheMemory
    {
        Task<object> SetCacheAsync(object input, int days, IMemoryCache _memoryCache);
    }
}
