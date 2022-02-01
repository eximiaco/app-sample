using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Matrix.AppSample.SeedWork
{
    public interface IServiceBus : IDisposable
    {
        Task SendAsync<TMessage>(TMessage message, string queueOrTopic = "", CancellationToken cancellationToken = default);
        Task<Result> SendAsync<TMessage>(string queueOrTopic, IEnumerable<TMessage> messages, CancellationToken cancellationToken = default);
    }
}
