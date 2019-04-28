using HRAssistant.Infrastructure.CQRS;

namespace HRAssistant.Admin.Contracts
{
    public sealed class UpdateUser : ICommand
    {
        public User User { get; set; }
    }
}
