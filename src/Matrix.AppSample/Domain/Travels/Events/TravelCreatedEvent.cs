using System;
using Matrix.AppSample.SeedWork;

namespace Matrix.AppSample.Domain.Travels.Events
{
    public sealed class TravelCreatedEvent : IDomainEvent
    {
        public TravelCreatedEvent(Guid travelId)
        {
            TravelId = travelId;
        }

        public Guid TravelId { get; }
    }
}