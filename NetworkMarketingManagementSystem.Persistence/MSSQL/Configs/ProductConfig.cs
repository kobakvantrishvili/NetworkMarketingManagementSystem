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
    internal class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product"); //not necessary since Table has the same name as our class

            //PK
            builder.HasKey(x => x.Id); // not necessary EF automatically identifies Id as Primary Key

            builder.Property(x => x.Code).HasMaxLength(50); 
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Price); //not necessary to write it, since no constraints are set

            //Related Types
            builder.HasMany(x => x.SaleProducts).WithOne(x => x.Product).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
