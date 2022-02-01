using Autofac;
using System.Reflection;
using Matrix.AppSample.Domain.Travels.Commands;
using Matrix.AppSample.SeedWork;
using Module = Autofac.Module;

namespace Matrix.AppSample.HttpApi.Infrastructure.AutofacModules
{
    internal sealed class AplicacaoModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(CreateTravelCommand).GetTypeInfo().Assembly;

            builder
                .RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IDomainService<>))
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRepository<>))
                .InstancePerLifetimeScope();
            
            builder
                .RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IQuery<>))
                .InstancePerLifetimeScope();
        }
    }
}
