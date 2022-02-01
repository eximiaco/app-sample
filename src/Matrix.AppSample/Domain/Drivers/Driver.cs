using System;
using Matrix.AppSample.SeedWork;

namespace Matrix.AppSample.Domain.Drivers
{
    public sealed class Driver : Entity<Guid>
    {
        public string Name { get; }
        public string Cpf { get; }
    }
}