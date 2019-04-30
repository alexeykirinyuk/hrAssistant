using System;
using Autofac;
using HRAssistant.DataAccess;
using HRAssistant.Infrastructure.CQRS;
using Microsoft.EntityFrameworkCore;

namespace HRAssistant.Tests
{
    public abstract class Tests : IDisposable
    {
        private const string ConnectionString =
            "Server=localhost\\SQLEXPRESS;Initial Catalog=HRAssistant;Integrated Security=SSPI;";

        private readonly IContainer _container;
        private readonly ILifetimeScope _lifetimeScope;
        private bool _isDisposed;

        protected IBus Bus { get; }

        protected Tests()
        {
            var context = new ContainerBuilder();
            context.RegisterModule<AutofacModule>();
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlServer(ConnectionString)
                .Options;

            context.RegisterType<DatabaseContext>().AsSelf().InstancePerLifetimeScope();
            context.RegisterInstance(options);
            _container = context.Build();
            _lifetimeScope = _container.BeginLifetimeScope();

            Bus = _container.Resolve<IBus>();
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _lifetimeScope.Dispose();
                _container.Dispose();
                _isDisposed = true;
            }
        }
    }
}
