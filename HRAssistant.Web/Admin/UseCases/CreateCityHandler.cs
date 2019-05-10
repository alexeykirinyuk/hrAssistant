using System.Threading.Tasks;
using HRAssistant.Infrastructure.CQRS;
using HRAssistant.Web.Admin.Contracts.CityContracts;

namespace HRAssistant.Web.Admin.UseCases
{
    public sealed class CreateCityHandler : ICommandHandler<CreateCity>
    {
        public Task Handle(CreateCity request)
        {
            return Task.CompletedTask;
        }
    }
}
