
using ItemService.Data;
using ItemService.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace ItemService.Services
{
    public class RestauranteService : IRestauranteService
    {
        private readonly IDistributedCache _cache;
        private readonly TimeSpan _cacheExpiry = TimeSpan.FromMinutes(1);
        private readonly IItemRepository _repository;

        public RestauranteService(IDistributedCache cache, IItemRepository repository)
        {
            _cache = cache;
            _repository = repository;
        }

        public async Task<IEnumerable<Restaurante>> GetAllRestaurante()
        {
            string cacheKey = $"Restaurante_All";

            // Step 1: Try to get the item from Redis cache
            var cachedItem = await _cache.GetStringAsync(cacheKey);
            if (cachedItem != null)
            {
      
                var restaurantesFromCache = JsonConvert.DeserializeObject<IEnumerable<Restaurante>>(cachedItem);
                Console.WriteLine($"{cachedItem}");
                Console.WriteLine("✅ Retaurante retrieved from cache!");
                return restaurantesFromCache;
            }

            // Step 2: Simulate database fetch (or real DB call in a production app)
            var restaurantes = _repository.GetAllRestaurantes();

            // Step 3: Cache the item in Redis
            if (restaurantes.Any())
            {
                await _cache.SetStringAsync(
                    cacheKey,
                    JsonConvert.SerializeObject(restaurantes),
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = _cacheExpiry,
                        SlidingExpiration = TimeSpan.FromMinutes(2) // Redefine no acesso
                    }
                );
                 Console.WriteLine("🔄 Restaurantes added to cache.");
            }



            return restaurantes;
        }
    }
}
