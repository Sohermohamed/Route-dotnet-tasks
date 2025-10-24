using ECommerce.Domain.Contracts.Seeds;
using ECommerce.Domain.Models;
using ECommerce.Domain.Models.Products;
using ECommerce.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Persistence.DataSeed
{
    public class DataSeeding : IDataSeeding
    {
        private readonly StoreDbContext context;

        public DataSeeding(StoreDbContext context)
        {
            this.context = context;
        }
        public void DataSeed()
        {
            if(context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();

            if(!context.ProductBrands.Any())
            {
                var ProductBrandData = File.ReadAllText(@"..\Infrastructure\ECommerce.Persistence\Data\brands.json");
                var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandData);
                  if(ProductBrands != null && ProductBrands.Any())
                {
                    context.ProductBrands.AddRange(ProductBrands);
                }
            }
            if (!context.ProductTypes.Any())
            {
                var ProductTypeData = File.ReadAllText(@"..\Infrastructure\ECommerce.Persistence\Data\types.json");
                var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypeData);
                if (ProductTypes != null && ProductTypes.Any())
                {
                    context.ProductTypes.AddRange(ProductTypes);
                }
            }
            if (!context.Products.Any())
            {
                var ProductData = File.ReadAllText(@"..\Infrastructure\ECommerce.Persistence\Data\products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                if (Products != null && Products.Any())
                {
                    context.Products.AddRange(Products);
                }
            }

            context.SaveChanges();

        }
    }
}
