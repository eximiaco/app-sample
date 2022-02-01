using Matrix.AppSample.Domain.Drivers;
using Matrix.AppSample.Domain.Travels;
using Matrix.AppSample.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Matrix.AppSample.Infrastructure.EFCore.EntityConfigurations
{
    public sealed class TravelMap : IEntityTypeConfiguration<Travel>
    {
        public void Configure(EntityTypeBuilder<Travel> builder)
        {
            builder.ToTable("Travels");
            builder.HasKey(a => a.Id);

            builder
                .HasOne<Driver>()
                .WithMany()
                .HasForeignKey(c => c.DriverId);
            
            builder
                .HasOne<Vehicle>()
                .WithMany()
                .HasForeignKey(c => c.VehicleId);
            
            builder
                .HasMany(n => n.StoppingPoints)
                .WithOne()
                .HasForeignKey("TravelId")
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata
                .PrincipalToDependent
                .SetField("_stoppingPoints");
        }
    }
}