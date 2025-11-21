using ECommerce.Domain.Contracts;
using ECommerce.Domain.Models.Baskets;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Repos
{
    public class BasketReposatory(IConnectionMultiplexer connection) : IBasketReposatory
    {
        private readonly IDatabase database = connection.GetDatabase();
        public async Task<CustomerBasket?> GetBasketAsync(string Key)
        {
            var basket = await database.StringGetAsync(Key);

            if(basket.IsNullOrEmpty)
            {
                return null;
            }
            return JsonSerializer.Deserialize<CustomerBasket>(basket);
        }
        public async Task<CustomerBasket?> CreateUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);

            var isCreatedOrUpdated = await database.StringSetAsync(basket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromHours(5));

            if(isCreatedOrUpdated)
            {
                return await GetBasketAsync(basket.Id);
            }
            return null;
        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
            return await database.KeyDeleteAsync(Key); 
        }
    }
}
