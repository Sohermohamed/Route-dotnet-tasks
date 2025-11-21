using ECommerce.Domain.Models.Products;
using ECommerce.Shared.Common;
using ECommerce.Shared.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specifications
{
    public class CountProductSpecifications : BaseSpecifications<Product, int>
    {
        public CountProductSpecifications(ProductQueryParams productQuery) :
           base(p => (!productQuery.BrandId.HasValue || p.BrandId == productQuery.BrandId) && (!productQuery.TypeId.HasValue || p.TypeId == productQuery.TypeId)
            && (string.IsNullOrEmpty(productQuery.SearchValue) || p.Name.ToLower().Contains(productQuery.SearchValue.ToLower())))
        {
        }
    }
}
