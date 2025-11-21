using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.DTO_s
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        
        public string PictureURL { get; set; } = null!;
        public decimal Price { get; set; }
        public string? BrandName { get; set; } = null!;
        public string TypeName { get; set; } = null!;

    }
}
