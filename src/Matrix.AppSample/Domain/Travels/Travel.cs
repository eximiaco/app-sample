using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using Matrix.AppSample.Domain.Shared;
using Matrix.AppSample.Domain.Travels.Events;

namespace Matrix.AppSample.Domain.Travels
{
    public sealed class Travel : SeedWork.Entity<Guid>
    {
        private Travel(){}
        private Travel(Guid id, Guid driverId, Guid vehicleId, List<StoppingPoint> stoppingPoints) 
            : base(id)
        {
            _stoppingPoints = stoppingPoints;
            VehicleId = vehicleId;
            DriverId = driverId;
        }

        private List<StoppingPoint> _stoppingPoints;
        
        public Guid DriverId { get; }
        public Guid VehicleId { get; }
        public IEnumerable<StoppingPoint> StoppingPoints => _stoppingPoints.AsReadOnly();

        public void AddOrUpdateStoppingPoint(Address address)
        {
            var stoppingPoint = _stoppingPoints.FirstOrDefault(c => c.Address.Equals(address));
            if (stoppingPoint == null)
            {
                var newStoppingPoint = StoppingPoint.Create(address);
                _stoppingPoints.Add(newStoppingPoint);
                AddDomainEvent(new StopingPointAddedToTravelEvent(Id, newStoppingPoint.Id, address));
            }
        }
        
        public static Result<Travel> Create(Guid driverId, Guid vehicleId)
        {
            var result = Result.Combine(
                Result.FailureIf(driverId == Guid.Empty, "Motorista inválido"),
                Result.FailureIf(vehicleId == Guid.Empty, "Veículo inválido"));
            if (result.IsFailure)
                return Result.Failure<Travel>(result.Error);
            
            var travel = new Travel(Guid.NewGuid(), driverId, vehicleId, 
                new List<StoppingPoint>(capacity:1));
            travel.AddDomainEvent(new TravelCreatedEvent(travel.Id));

            return travel;
        }
    }
}