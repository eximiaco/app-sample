using System.Threading;
using System.Threading.Tasks;
using Matrix.AppSample.Domain.Travels.Events;
using MediatR;

namespace Matrix.AppSample.Domain.Travels.Handlers
{
    public sealed class StoppingPointAddedToTravelEventHandler : INotificationHandler<StopingPointAddedToTravelEvent>
    {
        public async Task Handle(StopingPointAddedToTravelEvent notification, CancellationToken cancellationToken)
        {
            // Notificar via service broker outros contextos e sistemas
            
        }
    }
}