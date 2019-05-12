using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Admin.Contracts.UserContracts
{
    public sealed class UpdateUser : ICommand
    {
        public User User { get; set; }
    }
}
