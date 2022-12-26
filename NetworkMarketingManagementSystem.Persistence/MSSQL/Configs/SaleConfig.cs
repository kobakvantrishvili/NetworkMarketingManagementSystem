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
    internal class SaleConfig : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sale");

            //PK
            builder.HasKey(x => x.Id);

            builder.Property(x => x.SaleDate).IsRequired().HasColumnType("date");
            builder.Property(x => x.TotalPrice).IsRequired();

            //Related Types
            builder.HasOne(x => x.Distributor).WithMany(x => x.Sales).HasForeignKey(x => x.DistributorId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(x => x.SaleProducts).WithOne(x => x.Sale).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
