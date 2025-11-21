using ECommerce.Shared.DTO_s.BasketDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models.Baskets
{
    public class BasketDto
    {
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; } 
    }
}
