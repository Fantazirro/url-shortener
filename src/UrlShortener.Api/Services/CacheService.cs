using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace UrlShortenerApi.Services
{
    public class CacheService(IDistributedCache cache)
    {
        public record CacheItem<T>(bool Success, T Value);

        public async Task<CacheItem<T>> GetAsync<T>(string key)
        {
            var serializedValue = await cache.GetStringAsync(key);
            if (serializedValue is null) return new(false, default!);

            var value = JsonSerializer.Deserialize<T>(serializedValue)!;
            return new(true, value);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expirationTime)
        {
            var serializedValue = JsonSerializer.Serialize(value);

            await cache.SetStringAsync(key, serializedValue, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = expirationTime
            });
        }
    }
}