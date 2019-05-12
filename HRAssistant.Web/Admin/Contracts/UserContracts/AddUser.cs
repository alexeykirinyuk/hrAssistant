using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Admin.Contracts.UserContracts
{
    public sealed class AddUser : ICommand<AddUserResult>
    {
        public User User { get; set; }
    }
}
