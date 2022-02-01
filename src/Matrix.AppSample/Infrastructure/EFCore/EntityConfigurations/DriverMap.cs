using Matrix.AppSample.Domain.Drivers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Matrix.AppSample.Infrastructure.EFCore.EntityConfigurations
{
    public sealed class DriverMap : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.ToTable("Drivers");
            builder.HasKey(a => a.Id);

            builder.Property(c => c.Name);
            builder.Property(c => c.Cpf);
        }
    }
}