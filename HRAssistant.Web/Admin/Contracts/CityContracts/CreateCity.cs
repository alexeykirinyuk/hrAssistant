using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Admin.Contracts.CityContracts
{
    public sealed class CreateCity : ICommand<CreateCityResult>
    {
        public City City { get; set; }
    }
}
