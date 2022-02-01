using System;
using Matrix.AppSample.SeedWork;

namespace Matrix.AppSample.Domain.Vehicles
{
    public sealed class Vehicle : Entity<Guid>
    {
        public string LicensePlate { get; }
        public int Capacity { get;  }
    }
}