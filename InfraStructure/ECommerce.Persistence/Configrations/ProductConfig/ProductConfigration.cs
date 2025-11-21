using ECommerce.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Configrations.ProductConfig
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.Brand)
                   .WithMany()
                   .HasForeignKey(p => p.BrandId);

            builder.HasOne(p => p.Type)
                   .WithMany()
                   .HasForeignKey(p => p.TypeId);

            builder.Property(p => p.Price).HasColumnType("decimal(10,3)");
        }
    }
}
