using AutoMapper;
using ECommerce.Abstraction.IServices;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Models.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Services
{
    public class BasketServices(IBasketReposatory reposatory , IMapper mapper) : IBasketServices
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var CustomerBasket = mapper.Map<BasketDto, CustomerBasket>(basket); 
            var SavedBasket = await reposatory.CreateUpdateBasketAsync(CustomerBasket);

            if(SavedBasket != null)
            {
                return await GetBasketAsync(SavedBasket.Id);
            }
            else
            {
                throw new Exception("Something Went Wrong At Process");
            }
        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
            return await reposatory.DeleteBasketAsync(Key);
        }

        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var basket = await reposatory.GetBasketAsync(Key);
            if(basket != null)
            {
                return mapper.Map<CustomerBasket, BasketDto>(basket);
            }
            else
            {
                throw new BasketNotFoundException(Key);
            }
        }
    }
}
