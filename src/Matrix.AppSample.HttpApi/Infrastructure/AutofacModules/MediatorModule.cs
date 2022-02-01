using Autofac;
using MediatR;
using System.Reflection;
using Matrix.AppSample.Behaviors;
using Matrix.AppSample.Domain.Travels.Commands;
using Module = Autofac.Module;

namespace Matrix.AppSample.HttpApi.Infrastructure.AutofacModules
{
    internal class MediatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                  .AsImplementedInterfaces()
                  .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(CreateTravelCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(CreateTravelCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t =>
                {
                    object o;
                    return componentContext.TryResolve(t, out o) ? o : null;
                };
            });

            builder
               .RegisterGeneric(typeof(EnrichLogContextBehavior<,>))
               .As(typeof(IPipelineBehavior<,>))
               .InstancePerDependency();

            builder
                .RegisterGeneric(typeof(UnitOfWorkBehavior<,>))
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerDependency();

            builder
               .RegisterGeneric(typeof(UnitOfWorkWithTransactionBehavior<,>))
               .As(typeof(IPipelineBehavior<,>))
               .InstancePerDependency();
        }
    }
}
