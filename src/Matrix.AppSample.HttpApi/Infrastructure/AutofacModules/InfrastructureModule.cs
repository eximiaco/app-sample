using Autofac;
using Matrix.AppSample.Infrastructure.EFCore;
using Matrix.AppSample.Infrastructure.ServiceBus;
using Matrix.AppSample.SeedWork;
using Module = Autofac.Module;

namespace Matrix.AppSample.HttpApi.Infrastructure.AutofacModules
{
    public sealed class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
               .RegisterType<MatrixAppDBContextFactory>()
               .As<IEFDbContextFactory<MatrixAppDBContext>>()
               .InstancePerLifetimeScope();

            builder
                .RegisterType<MatrixAppDBContextAccessor>()
                .As<IEFDbContextAccessor<MatrixAppDBContext>>()
                .InstancePerLifetimeScope();

            builder
               .RegisterType<AzureServiceBusService>()
               .As<IServiceBus>()
               .InstancePerLifetimeScope();
        }
    }
}
