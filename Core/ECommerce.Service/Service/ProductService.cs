using AutoMapper;
using ECommerce.Abstraction.Services;
using ECommerce.Domain.Contracts.UOW;
using ECommerce.Domain.Models;
using ECommerce.Domain.Models.Products;
using ECommerce.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Service
{
    class ProductService(IUnitOfWork UnitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Repo = UnitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            var BrandDtos = mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
            return BrandDtos;

        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var Product = UnitOfWork.GetRepository<Product, int>();
            var Products = await Product.GetAllAsync();
            var ProductDtos = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
            return ProductDtos;

        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Type = UnitOfWork.GetRepository<ProductType, int>();
            var Types = await Type.GetAllAsync();
            var TypeDtos = mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
            return TypeDtos;

        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var Product = await UnitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            return mapper.Map<Product, ProductDto>(Product);
        }
    
    }
}
