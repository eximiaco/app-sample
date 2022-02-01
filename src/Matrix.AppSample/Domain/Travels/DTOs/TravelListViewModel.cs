using System;

namespace Matrix.AppSample.Domain.Travels.DTOs
{
    public sealed class TravelListViewModel
    {
        public Guid Id { get; }
        public string LicensePlate { get; }
        public string DriverName { get; }
        public int StoppingPointsCount { get; }
    }
}