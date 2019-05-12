using System;
using Autofac;
using HRAssistant.Web;
using HRAssistant.Web.DataAccess;
using HRAssistant.Web.Infrastructure.CQRS;
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
        protected TestApi TestApi { get; }

        protected Tests()
        {
            var context = new ContainerBuilder();
            context.RegisterModule<AutofacModule>();
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlServer(ConnectionString)
                .Options;

            context.RegisterType<DatabaseContext>().AsSelf().InstancePerLifetimeScope();
            context.RegisterInstance(options);
            context.RegisterType<TestApi>();
            _container = context.Build();
            _lifetimeScope = _container.BeginLifetimeScope();

            Bus = _container.Resolve<IBus>();
            TestApi = _container.Resolve<TestApi>();
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
