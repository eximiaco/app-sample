using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Matrix.AppSample.Infrastructure.EFCore;
using Matrix.AppSample.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Matrix.AppSample.Domain.Vehicles
{
    public sealed class VehiclesRepository : IRepository<VehiclesRepository>
    {
        private readonly IEFDbContextAccessor<MatrixAppDBContext> _dbContextAccessor;

        public VehiclesRepository(IEFDbContextAccessor<MatrixAppDBContext> dbContextAccessor)
        {
            _dbContextAccessor = dbContextAccessor;
        }

        public IUnitOfWork UnitOfWork => _dbContextAccessor.Get();

        public async Task<Maybe<Vehicle>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContextAccessor
                .Get()
                .Vehicles
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }
    }
}