using System;
using Matrix.AppSample.Domain.Shared;
using Matrix.AppSample.SeedWork;

namespace Matrix.AppSample.Domain.Travels
{
    public sealed class StoppingPoint : Entity<Guid>
    {
        public StoppingPoint(Guid id, Address address)
            :base(id)
        {
            Address = address;
        }

        public Address Address { get; }

        public static StoppingPoint Create(Address address)
            => new StoppingPoint(Guid.NewGuid(), address);
    }
}