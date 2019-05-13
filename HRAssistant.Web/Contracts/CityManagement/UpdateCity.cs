using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.CityManagement
{
    public sealed class UpdateCity : ICommand
    {
        public City City { get; set; }
    }
}
