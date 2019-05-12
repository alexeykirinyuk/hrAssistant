using System.Threading.Tasks;

namespace HRAssistant.Web.Infrastructure.CQRS
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        Task Handle(TCommand request);
    }

    public interface ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        Task<TResult> Handle(TCommand command);
    }
}
