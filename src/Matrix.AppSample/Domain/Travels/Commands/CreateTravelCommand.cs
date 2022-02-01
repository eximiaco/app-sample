using System;
using CSharpFunctionalExtensions;
using Matrix.AppSample.SeedWork;
using MediatR;

namespace Matrix.AppSample.Domain.Travels.Commands
{
    public sealed class CreateTravelCommand : ICommand, IRequest<Result<Guid>>
    {
        private CreateTravelCommand(Guid vehicleId, Guid driverId)
        {
            VehicleId = vehicleId;
            DriverId = driverId;
        }

        public Guid VehicleId { get; }
        public Guid DriverId { get; }

        public static CreateTravelCommand Create(Guid vehicleId, Guid driverId)
            => new CreateTravelCommand(vehicleId, driverId);
    }
}