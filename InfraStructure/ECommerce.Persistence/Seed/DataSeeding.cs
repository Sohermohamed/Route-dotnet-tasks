using ECommerce.Domain.Contracts.Seed;
using ECommerce.Domain.Models.Products;
using ECommerce.Persistence.Contexts;
using ECommerce.Persistence.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Seed
{
    public class DataSeeding(StoreDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if(!context.ProductBrands.Any())
            {
                var BrandData = await File.ReadAllTextAsync(@"..\InfraStructure\ECommerce.Persistence\Data\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);

                if(brands != null && brands.Any())
                {
                    context.ProductBrands.AddRange(brands);
                }
            }

            if (!context.ProductTypes.Any())
            {
                var TypeData = await File.ReadAllTextAsync(@"..\InfraStructure\ECommerce.Persistence\Data\types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);

                if (Types != null && Types.Any())
                {
                    context.ProductTypes.AddRange(Types);
                }
            }

            if (!context.Products.Any())
            {
                var ProductsData = await File.ReadAllTextAsync(@"..\InfraStructure\ECommerce.Persistence\Data\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if (products != null && products.Any())
                {
                    context.Products.AddRange(products);
                }
            }

            context.SaveChanges();
        }

        public async Task IdentitySeedAsync()
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!userManager.Users.Any())
                {
                    var User1 = new ApplicationUser()
                    {
                        Email = "alyaa@gmail.com",
                        DisplayName = "Alyaa",
                        PhoneNumber = "01000000000",
                        UserName = "AlyaaTamer"

                    };
                    var User2 = new ApplicationUser()
                    {
                        Email = "aly@gmail.com",
                        DisplayName = "Aly",
                        PhoneNumber = "01230000000",
                        UserName = "AlyTamer"
                    };
                    await userManager.CreateAsync(User1, "P@ssw0rd");
                    await userManager.CreateAsync(User2, "P@ssw0rd");

                    await userManager.AddToRoleAsync(User1, "Admin");
                    await userManager.AddToRoleAsync(User2, "SuperAdmin");
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
