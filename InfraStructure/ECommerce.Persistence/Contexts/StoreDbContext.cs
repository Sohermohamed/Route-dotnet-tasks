using ECommerce.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Contexts
{
    public class StoreDbContext : DbContext
    {

        public StoreDbContext(DbContextOptions<StoreDbContext> options): base(options) 
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
        }

        public DbSet<Product> Products { get; set; } 
        public DbSet<ProductBrand> ProductBrands { get; set; } 
        public DbSet<ProductType> ProductTypes { get; set; } 
    }
}
