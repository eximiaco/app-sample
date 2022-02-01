using System;
using CSharpFunctionalExtensions;
using Matrix.AppSample.SeedWork;
using MediatR;

namespace Matrix.AppSample.Domain.Travels.Commands
{
    public sealed  class AddStoppingPointCommand: ICommand, IRequest<Result<Guid>>
    {
        
    }
}