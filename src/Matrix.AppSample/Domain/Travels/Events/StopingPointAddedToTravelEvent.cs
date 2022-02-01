using System;
using Matrix.AppSample.Domain.Shared;
using Matrix.AppSample.SeedWork;

namespace Matrix.AppSample.Domain.Travels.Events
{
    public sealed class StopingPointAddedToTravelEvent : IDomainEvent
    {
        public StopingPointAddedToTravelEvent(Guid travelId, Guid stoppingPointId, Address addressAdded)
        {
            TravelId = travelId;
            StoppingPointId = stoppingPointId;
            AddressAdded = addressAdded;
        }

        public Guid TravelId { get; }
        public Guid StoppingPointId { get; }
        public Address AddressAdded { get; }
    }
}