using CSharpFunctionalExtensions;
using System;
using System.ComponentModel.DataAnnotations;
using Matrix.AppSample.Domain.Travels.Commands;

namespace Matrix.AppSample.HttpApi.Models.Travels
{
    public sealed class CreateTravelInputModel
    {
        [Required]
        public Guid VehicleId { get; }
        public Guid DriverId { get; }

        public Result<CreateTravelCommand> CreateCommand()
        {
            if (VehicleId == Guid.Empty)
                return Result.Failure<CreateTravelCommand>("Veículo não informado.");
            if (DriverId == Guid.Empty)
                return Result.Failure<CreateTravelCommand>("Motorista não informado.");
            
            return CreateTravelCommand.Create(VehicleId, DriverId);
        }
    }
}
