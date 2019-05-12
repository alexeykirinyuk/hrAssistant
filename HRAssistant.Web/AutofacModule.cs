using Autofac;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.DataAccess.Repositories;
using HRAssistant.Web.Infrastructure;
using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web
{
    public sealed class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(ThisAssembly)
                .Where(assembly => assembly.Name.EndsWith("Validator") || assembly.Name.EndsWith("Handler"))
                .AsImplementedInterfaces()
                .AsSelf()
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
