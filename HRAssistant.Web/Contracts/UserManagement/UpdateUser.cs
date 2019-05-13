using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.UserManagement
{
    public sealed class UpdateUser : ICommand
    {
        public User User { get; set; }
    }
}
