using ECommerce.Domain.Models.Products;
using ECommerce.Shared.Common;
using System;

namespace ECommerce.Service.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product, int>
    {
        public ProductSpecifications(ProductQueryParams productQuery) :
            base(p => (!productQuery.BrandId.HasValue || p.BrandId == productQuery.BrandId) && (!productQuery.TypeId.HasValue || p.TypeId == productQuery.TypeId)
            && (string.IsNullOrEmpty(productQuery.SearchValue) || p.Name.ToLower().Contains(productQuery.SearchValue.ToLower())) )
        {
            AddIncludes(p => p.Brand);
            AddIncludes(p => p.Type);

            switch(productQuery.SortingOptions)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;
                default:
                    break;
            }

            ApplyPagination(productQuery.PageIndex, productQuery.PageSize);
        }
        public ProductSpecifications(int id) : base(p => p.Id == id)
        {
            AddIncludes(p => p.Brand);
            AddIncludes(p => p.Type);
        }
    }
}
