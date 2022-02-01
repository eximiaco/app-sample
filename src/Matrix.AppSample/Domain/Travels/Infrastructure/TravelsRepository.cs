using System.Threading;
using System.Threading.Tasks;
using Matrix.AppSample.Infrastructure.EFCore;
using Matrix.AppSample.SeedWork;

namespace Matrix.AppSample.Domain.Travels.Infrastructure
{
    public sealed class TravelsRepository : IRepository<TravelsRepository>
    {
        private readonly IEFDbContextAccessor<MatrixAppDBContext> _dbContextAccessor;

        public TravelsRepository(IEFDbContextAccessor<MatrixAppDBContext> dbContextAccessor)
        {
            _dbContextAccessor = dbContextAccessor;
        }

        public IUnitOfWork UnitOfWork => _dbContextAccessor.Get();

        public async Task AddAsync(Travel travel, CancellationToken cancellationToken = default)
        {
            await _dbContextAccessor.Get().AddAsync(travel, cancellationToken);
        }
    }
}