using HRAssistant.Infrastructure.CQRS;

namespace HRAssistant.Admin.Contracts.UserContracts
{
    public sealed class UpdateUser : ICommand
    {
        public User User { get; set; }
    }
}
