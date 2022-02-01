using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Matrix.AppSample.SeedWork;

namespace Matrix.AppSample.Behaviors
{
    public class EnrichLogContextBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<EnrichLogContextBehavior<TRequest, TResponse>> _logger;

        public EnrichLogContextBehavior(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<EnrichLogContextBehavior<TRequest, TResponse>>();
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse resposta;
            if (request.GetType().GetInterfaces().All(c => c != typeof(ICommand)))
                return await next();

            using (LogContext.PushProperty("CorrelationId", Guid.NewGuid()))
            {
                resposta = await next();
            }
            return resposta;
        }
    }
}
