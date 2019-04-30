using System;
using Autofac;
using HRAssistant.Infrastructure.CQRS;

namespace HRAssistant.Tests
{
    public abstract class Tests : IDisposable
    {
        private readonly IContainer _container;
        private bool _isDisposed;

        protected IBus Bus { get; }

        public Tests()
        {
            var context = new ContainerBuilder();
            context.RegisterModule<AutofacModule>();
            _container = context.Build();

            Bus = _container.Resolve<IBus>();
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _container.Dispose();
                _isDisposed = true;
            }
        }
    }
}
