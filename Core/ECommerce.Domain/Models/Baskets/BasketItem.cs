using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models.Baskets
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
