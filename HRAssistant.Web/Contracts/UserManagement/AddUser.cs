using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.UserManagement
{
    public sealed class AddUser : ICommand<AddUserResult>
    {
        public User User { get; set; }
    }
}
