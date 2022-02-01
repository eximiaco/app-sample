using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Matrix.AppSample.Domain.Drivers;
using Matrix.AppSample.Domain.Travels.Commands;
using Matrix.AppSample.Domain.Travels.Infrastructure;
using Matrix.AppSample.Domain.Vehicles;
using MediatR;

namespace Matrix.AppSample.Domain.Travels.Handlers
{
    public sealed class CrateTravelCommandHandler : IRequestHandler<CreateTravelCommand, Result<Guid>>
    {
        private readonly DriversRepository _driversRepository;
        private readonly VehiclesRepository _vehiclesRepository;
        private readonly TravelsRepository _travelsRepository;

        public CrateTravelCommandHandler(
            DriversRepository driversRepository,
            VehiclesRepository vehiclesRepository,
            TravelsRepository travelsRepository)
        {
            _driversRepository = driversRepository;
            _vehiclesRepository = vehiclesRepository;
            _travelsRepository = travelsRepository;
        }
        
        public async Task<Result<Guid>> Handle(CreateTravelCommand request, CancellationToken cancellationToken)
        {
            var driver = await _driversRepository.GetByIdAsync(request.DriverId, cancellationToken);
            if (driver.HasNoValue)
                return Result.Failure<Guid>("Motorista não foi localizado");

            var vehicle = await _vehiclesRepository.GetByIdAsync(request.VehicleId, cancellationToken);
            if(vehicle.HasNoValue)
                return Result.Failure<Guid>("Veículo não foi localizado");

            var travel = Travel.Create(request.DriverId, request.VehicleId);
            if(travel.IsFailure)
                return Result.Failure<Guid>(travel.Error);

            await _travelsRepository.AddAsync(travel.Value, cancellationToken);

            await _travelsRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return travel.Value.Id;
        }
    }
}