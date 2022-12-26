using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetworkMarketingManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMarketingManagementSystem.Persistence.Configs
{
    internal class SaleProductsConfig : IEntityTypeConfiguration<SaleProduct>
    {
        public void Configure(EntityTypeBuilder<SaleProduct> builder)
        {   

            builder.ToTable("SaleProduct");

            //PK
            builder.HasKey(x => new { x.SaleId, x.ProductId });

            builder.Property(x => x.ProductTotalPrice).IsRequired();

            //Related Types
            builder.HasOne(x => x.Sale).WithMany(x => x.SaleProducts).OnDelete(DeleteBehavior.Cascade).HasForeignKey(x => x.SaleId);
            builder.HasOne(x => x.Product).WithMany(x => x.SaleProducts).OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.ProductId);
        }
    }
}
