using AutoMapper;
using ECommerce.Abstraction.IServices;
using ECommerce.Domain.Contracts.UOW;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Models.Products;
using ECommerce.Service.Specifications;
using ECommerce.Shared.Common;
using ECommerce.Shared.DTO_s;

namespace ECommerce.Service.Services
{
    public class ProductServices(IUnitOfWork unitOfWork , IMapper mapper) : IProductServices
    {
        private readonly IMapper mapper = mapper;

        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var Repo = unitOfWork.GetRebosatory<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            var BrandDto = mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDTO>>(Brands);
            return BrandDto;
        }

        public async Task<PaginationResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams productQuery)
        {
            var Spec = new ProductSpecifications(productQuery);
            var Products = await unitOfWork.GetRebosatory<Product, int>().GetAllWithSpecificationsAsync(Spec);
            var Data = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(Products);
            var Size = Data.Count();
            var TotalSpec = new CountProductSpecifications(productQuery);
            var TotalCount = await unitOfWork.GetRebosatory<Product, int>().GetCountWithSpecificationsAsync(TotalSpec);

            return new PaginationResult<ProductDTO>(productQuery.PageIndex,Size, TotalCount, Data);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var Types = await unitOfWork.GetRebosatory<ProductType, int>().GetAllAsync();
            return mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDTO>>(Types);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var Spec = new ProductSpecifications(id);
            var Product = await unitOfWork.GetRebosatory<Product, int>().GetByIdWithSpecificationAsync(Spec);

            if (Product is null)
            {
                throw new ProductNotFound(id);
            }

            return mapper.Map<Product, ProductDTO>(Product);
        }
    }
}
