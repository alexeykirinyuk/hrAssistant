using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.CityManagement
{
    public sealed class CreateCity : ICommand<CreateCityResult>
    {
        public City City { get; set; }
    }
}
