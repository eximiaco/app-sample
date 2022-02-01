using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Matrix.AppSample.SeedWork;

namespace Matrix.AppSample.Infrastructure.EFCore
{
    internal static class MediatorExtension
    {
        internal static async Task DispatchDomainEventsAsync(this IMediator mediator, DbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<IEntity>()
                .Where(x => x.Entity?.DomainEvents?.Any() == true)
                .ToList();

            if (!domainEntities.Any())
                return;

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities
                .ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
