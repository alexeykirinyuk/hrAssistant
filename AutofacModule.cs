using Autofac;
using HRAssistant.DataAccess.Core;
using HRAssistant.DataAccess.Repositories;
using HRAssistant.Infrastructure;
using HRAssistant.Infrastructure.CQRS;

namespace HRAssistant
{
    public sealed class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(ThisAssembly)
                .Where(assembly => assembly.Name.EndsWith("Validator") || assembly.Name.EndsWith("Handler"))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder
                 .RegisterAssemblyTypes(ThisAssembly)
                 .Where(assembly => assembly.Name.EndsWith("Repository"))
                 .AsImplementedInterfaces()
                 .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Bus>()
                .As<IBus>()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Validator<>))
                .AsSelf()
                .InstancePerDependency();
        }
    }
}
