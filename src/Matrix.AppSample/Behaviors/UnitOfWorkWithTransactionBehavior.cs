using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Matrix.AppSample.Infrastructure.EFCore;
using Matrix.AppSample.SeedWork;

namespace Matrix.AppSample.Behaviors
{
    public sealed class UnitOfWorkWithTransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<UnitOfWorkWithTransactionBehavior<TRequest, TResponse>> _logger;
        private readonly IEFDbContextFactory<MatrixAppDBContext> _efDbContextFactory;
        private readonly IEFDbContextAccessor<MatrixAppDBContext> _efDbContextAccessor;

        public UnitOfWorkWithTransactionBehavior(
            ILogger<UnitOfWorkWithTransactionBehavior<TRequest, TResponse>> logger,
            IEFDbContextFactory<MatrixAppDBContext> efDbContextFactory,
            IEFDbContextAccessor<MatrixAppDBContext> efDbContextAccessor)
        {
            _logger = logger;
            _efDbContextFactory = efDbContextFactory;
            _efDbContextAccessor = efDbContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                if (request.GetType().GetInterfaces().All(c => c != typeof(ITransactionalCommand)))
                    return await next();

                TResponse resposta;
                using (var contexto = _efDbContextFactory.CreateWithTransaction())
                {
                    _efDbContextAccessor.Register(contexto);
                    resposta = await next();
                }
                return resposta;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Não foi possível processar comando.");
                throw;
            }
            finally
            {
                _efDbContextAccessor.Clear();
            }
        }
    }
}
