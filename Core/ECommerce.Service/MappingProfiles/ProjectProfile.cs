using AutoMapper;
using ECommerce.Domain.Models.Baskets;
using ECommerce.Domain.Models.Products;
using ECommerce.Persistence.Identity.Models;
using ECommerce.Shared.DTO_s;
using ECommerce.Shared.DTO_s.BasketDto_s;
using ECommerce.Shared.DTO_s.IdentityDto_s;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.MappingProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile(IConfiguration configuration)
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dist => dist.BrandName, options => options.MapFrom(scr => scr.Brand.Name))
                .ForMember(dist => dist.TypeName, options => options.MapFrom(scr => scr.Type.Name))
                .ForMember(dist => dist.PictureURL, options => options.MapFrom(new PictureUrlResolver(configuration)));

            CreateMap<ProductBrand, BrandDTO>();

            CreateMap<ProductType, TypeDTO>();

            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
