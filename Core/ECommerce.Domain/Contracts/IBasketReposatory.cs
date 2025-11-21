using ECommerce.Domain.Models.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts
{
    public interface IBasketReposatory
    {
        Task<CustomerBasket?> GetBasketAsync(string Key);
        Task<CustomerBasket?> CreateUpdateBasketAsync(CustomerBasket basket,TimeSpan? TimeToLive = null);
        Task<bool> DeleteBasketAsync(string Key);
    }
}
