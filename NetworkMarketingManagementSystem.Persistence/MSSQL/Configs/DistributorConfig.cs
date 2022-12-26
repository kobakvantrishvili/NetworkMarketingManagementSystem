using Microsoft.EntityFrameworkCore;
using NetworkMarketingManagementSystem.Domain.ForDistributor;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetworkMarketingManagementSystem.Persistence.Configs
{
    internal class DistributorConfig : IEntityTypeConfiguration<Distributor>
    {
        public void Configure(EntityTypeBuilder<Distributor> builder)
        {
            // so that table is called Distributor
            builder.ToTable("Distributor"); // not necessary since our class is already called Distributor

            //PK
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(x => x.Birthday).IsRequired().HasColumnType("date");
            builder.Property(x => x.Sex).IsRequired();
            builder.Property(x => x.Image).HasColumnType("varbinary(MAX)");
            builder.Property(x => x.ReferredBy);
            builder.Property(x => x.References);
            builder.Property(x => x.Level);

            //Related Types
            builder.HasMany(x => x.Sales).WithOne(x => x.Distributor).OnDelete(DeleteBehavior.NoAction);

            //Owned Types (these are part of distributor object as they don't exist without it)
            builder.OwnsOne(x => x.IdentityDocument, y =>
            {
                y.ToTable("IdentityDocument", "Distributor"); // scheme is now called Distributor so our table will be Distributor.IdentityDocument
                y.WithOwner(x=>x.Distributor).HasForeignKey(a => a.DistributorId);

                y.Property(a => a.Type).IsRequired();
                y.Property(a => a.Series).HasMaxLength(10).IsUnicode();
                y.Property(a => a.Number).HasMaxLength(10).IsUnicode();
                y.Property(a => a.ReleaseDate).IsRequired().HasColumnType("date");
                y.Property(a => a.ValidUntil).IsRequired().HasColumnType("date");
                y.Property(a => a.PersonalNumber).IsRequired().HasMaxLength(50).IsUnicode();
                y.Property(a => a.IssuingAgency).HasMaxLength(100).IsUnicode();
            });
            builder.OwnsOne(x => x.ContactInfo, y =>
            {
                y.ToTable("ContactInfo", "Distributor");
                y.WithOwner(x => x.Distributor).HasForeignKey(a => a.DistributorId);

                y.Property(a => a.Type).IsRequired();
                y.Property(a => a.Contact).HasMaxLength(100).IsUnicode();
            });
            builder.OwnsOne(x => x.AddressInfo, y =>
            {
                y.ToTable("AddressInfo", "Distributor");
                y.WithOwner(x => x.Distributor).HasForeignKey(a => a.DistributorId);

                y.Property(a => a.Type).IsRequired();
                y.Property(a => a.Address).HasMaxLength(100).IsUnicode();
            });
        }
    }
}
