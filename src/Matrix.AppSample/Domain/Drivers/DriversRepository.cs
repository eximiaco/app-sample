using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Matrix.AppSample.Infrastructure.EFCore;
using Matrix.AppSample.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Matrix.AppSample.Domain.Drivers
{
    public sealed class DriversRepository : IRepository<DriversRepository>
    {
        private readonly IEFDbContextAccessor<MatrixAppDBContext> _dbContextAccessor;

        public DriversRepository(IEFDbContextAccessor<MatrixAppDBContext> dbContextAccessor)
        {
            _dbContextAccessor = dbContextAccessor;
        }

        public IUnitOfWork UnitOfWork => _dbContextAccessor.Get();

        public async Task<Maybe<Driver>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContextAccessor
                .Get()
                .Drivers
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }
    }
}