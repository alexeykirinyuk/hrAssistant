using System.Threading.Tasks;
using Autofac;
using LiteGuard;

namespace HRAssistant.Web.Infrastructure.CQRS
{
    internal sealed class Bus : IBus
    {
        private readonly IComponentContext _componentContext;

        public Bus(IComponentContext componentContext)
        {
            Guard.AgainstNullArgument(nameof(componentContext), componentContext);

            _componentContext = componentContext;
        }

        public async Task<TResult> Execute<TResult>(IQuery<TResult> request)
        {
            Guard.AgainstNullArgument(nameof(request), request);

            await Validate(request);

            var queryHandlerType = typeof(IQueryHandler<,>)
                .MakeGenericType(new[] { request.GetType(), typeof(TResult) });
            var queryHandler = _componentContext.Resolve(queryHandlerType);

            return await (Task<TResult>)queryHandler
                .GetType()
                .GetMethod("Handle", new[] { request.GetType() })
                .Invoke(queryHandler, new[] { request });
        }

        public async Task Request(ICommand request)
        {
            Guard.AgainstNullArgument(nameof(request), request);

            await Validate(request);

            var commandHandlerType = typeof(ICommandHandler<>)
                .MakeGenericType(new[] { request.GetType() });
            var commandHandler = _componentContext.Resolve(commandHandlerType);

            await (Task)commandHandler
                .GetType()
                .GetMethod("Handle", new[] { request.GetType() })
                .Invoke(commandHandler, new[] { request });
        }

        public async Task<TResult> Request<TResult>(ICommand<TResult> request)
        {
            Guard.AgainstNullArgument(nameof(request), request);

            await Validate(request);

            var commandHandlerType = typeof(ICommandHandler<,>)
                .MakeGenericType(new[] { request.GetType(), typeof(TResult) });
            var commandHandler = _componentContext.Resolve(commandHandlerType);

            return await (Task<TResult>)commandHandler
                .GetType()
                .GetMethod("Handle", new[] { request.GetType() })
                .Invoke(commandHandler, new[] { request });
        }

        private async Task Validate(object request)
        {
            var requestType = request.GetType();
            var validatorType = typeof(Validator<>).MakeGenericType(requestType);

            var validator = _componentContext.Resolve(validatorType);
            var validateMethod = validator.GetType().GetMethod(nameof(Validator<int>.Validate));
            var task = (Task)validateMethod.Invoke(validator, new object[] { request });

            await task;
        }
    }
}
