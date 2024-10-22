using Microsoft.Extensions.Caching.Distributed;

using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache cache) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasketAsync(string username, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(username, cancellationToken);
            if (!string.IsNullOrEmpty(cachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }

            var basket = await basketRepository.GetBasketAsync(username, cancellationToken);
            await cache.SetStringAsync(username, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await basketRepository.StoreBasketAsync(basket, cancellationToken);
            await cache.SetStringAsync(basket.Username, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task<bool> DeleteBasketAsync(string username, CancellationToken cancellationToken = default)
        {
            await basketRepository.DeleteBasketAsync(username, cancellationToken);
            await cache.RemoveAsync(username, cancellationToken);
            return true;
        }
    }
}
