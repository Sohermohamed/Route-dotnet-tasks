using ECommerce.Abstraction.IServices;
using ECommerce.Domain.Models.Baskets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class BasketController(IServicesManger services) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string Key)
        {
            var Basket = await services.BasketServices.GetBasketAsync(Key);
            return Ok(Basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateUpdateBasket(BasketDto basket)
        {
            var Basket = await services.BasketServices.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }

        [HttpDelete("{Key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        {
            var Basket = await services.BasketServices.DeleteBasketAsync(Key);
            return Ok(Basket);
        }

    }
}
