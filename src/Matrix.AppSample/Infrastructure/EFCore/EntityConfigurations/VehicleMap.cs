using Matrix.AppSample.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Matrix.AppSample.Infrastructure.EFCore.EntityConfigurations
{
    public sealed class VehicleMap : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicles");
            builder.HasKey(a => a.Id);

            builder.Property(c => c.LicensePlate);
            builder.Property(c => c.Capacity);
        }
    }
}