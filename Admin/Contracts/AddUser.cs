using HRAssistant.Infrastructure.CQRS;

namespace HRAssistant.Admin.Contracts
{
    public sealed class AddUser : ICommand<AddUserResult>
    {
        public User User { get; set; }
    }
}
