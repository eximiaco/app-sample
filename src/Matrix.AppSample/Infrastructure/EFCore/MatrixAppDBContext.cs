using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Matrix.AppSample.Domain.Drivers;
using Matrix.AppSample.Domain.Travels;
using Matrix.AppSample.Domain.Vehicles;
using Matrix.AppSample.Infrastructure.EFCore.EntityConfigurations;
using Matrix.AppSample.SeedWork;

namespace Matrix.AppSample.Infrastructure.EFCore
{
    public sealed class MatrixAppDBContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public MatrixAppDBContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Travel> Travels { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            await _mediator.DispatchDomainEventsAsync(this);
            return result > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TravelMap());
            modelBuilder.ApplyConfiguration(new StoppingPointMap());
            modelBuilder.ApplyConfiguration(new DriverMap());
            modelBuilder.ApplyConfiguration(new VehicleMap());
        }
    }
}
