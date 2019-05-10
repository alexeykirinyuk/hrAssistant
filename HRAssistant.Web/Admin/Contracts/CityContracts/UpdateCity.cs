using HRAssistant.Infrastructure.CQRS;

namespace HRAssistant.Web.Admin.Contracts.CityContracts
{
    public sealed class UpdateCity : ICommand
    {
        public City City { get; set; }
    }
}
