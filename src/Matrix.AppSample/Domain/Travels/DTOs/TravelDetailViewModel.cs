using System;
using System.Collections;
using System.Collections.Generic;

namespace Matrix.AppSample.Domain.Travels.DTOs
{
    public sealed  class TravelDetailViewModel
    {
        public Guid Id { get; }
        public string DriverName { get; }
        public string VehicleLicensePlate { get; }
        public IEnumerable<StoppingPointDetailViewModel> StoppingPoints { get; }

        public sealed class StoppingPointDetailViewModel
        {
            public Guid Id { get; }
            public string FullAddress { get; }
        }
    }
}