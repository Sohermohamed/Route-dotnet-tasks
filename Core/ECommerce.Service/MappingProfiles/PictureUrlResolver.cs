using AutoMapper;
using AutoMapper.Execution;
using ECommerce.Domain.Models.Products;
using ECommerce.Shared.DTO_s;
using Microsoft.Extensions.Configuration;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.MappingProfiles
{
    public class PictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductDTO, string>
    {
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureURL))
            {
                return string.Empty;
            }
            else
            {
                var url = $"{configuration.GetSection("Urls")["BaseUrl"]}{source.PictureURL}";
                return url;
            }
        }
    }
}
