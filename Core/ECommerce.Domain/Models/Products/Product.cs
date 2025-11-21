using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models.Products
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureURL { get; set; } = null!;
        public decimal Price { get; set; }

        public int BrandId { get; set; }
        public ProductBrand Brand { get; set; } = null!;

        public int TypeId { get; set; }
        public ProductType Type { get; set; } = null!;

    }
}
