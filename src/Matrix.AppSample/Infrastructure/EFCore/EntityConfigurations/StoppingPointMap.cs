using Matrix.AppSample.Domain.Travels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Matrix.AppSample.Infrastructure.EFCore.EntityConfigurations
{
    public sealed class StoppingPointMap: IEntityTypeConfiguration<StoppingPoint>
    {
        public void Configure(EntityTypeBuilder<StoppingPoint> builder)
        {
            builder.ToTable("StoppingPoints");
            builder.HasKey(a => a.Id);
            
            builder.OwnsOne(a => a.Address, address =>
            {
                address.Property(a => a.Street).HasColumnName("AddressStreet");
                address.Property(a => a.Number).HasColumnName("AddressNumber");
                address.Property(a => a.Complement).HasColumnName("AddressComplement");
                address.Property(a => a.Neighborhood).HasColumnName("AddressNeighborhood");
                address.Property(a => a.Cep).HasColumnName("AddressCep");
                address.Property(a => a.City).HasColumnName("AddressCity");
                address.Property(a => a.State).HasColumnName("AddressState");
            });
        }
    }
}