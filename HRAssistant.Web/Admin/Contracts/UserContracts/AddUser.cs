using HRAssistant.Infrastructure.CQRS;

namespace HRAssistant.Admin.Contracts.UserContracts
{
    public sealed class AddUser : ICommand<AddUserResult>
    {
        public User User { get; set; }
    }
}
