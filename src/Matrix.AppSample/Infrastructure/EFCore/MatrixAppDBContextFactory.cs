using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Matrix.AppSample.Infrastructure.EFCore
{
    public sealed class MatrixAppDBContextFactory : IEFDbContextFactory<MatrixAppDBContext>
    {
        private readonly string _connectionString;
        private readonly IMediator _mediator;

        public MatrixAppDBContextFactory(IConfiguration configuration, IMediator mediator)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _mediator = mediator;
        }

        public MatrixAppDBContext Create()
        {
            var options = new DbContextOptionsBuilder<MatrixAppDBContext>()
                              .UseSqlServer(_connectionString, options => options.EnableRetryOnFailure())
                              .Options;
            return new MatrixAppDBContext(options, _mediator);
        }

        public MatrixAppDBContext CreateWithTransaction()
        {
            var options = new DbContextOptionsBuilder<MatrixAppDBContext>()
                              .UseSqlServer(_connectionString)
                              .Options;
            return new MatrixAppDBContext(options, _mediator);
        }
    }
}
