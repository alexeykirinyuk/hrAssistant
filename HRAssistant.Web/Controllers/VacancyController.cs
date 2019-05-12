using System.Threading.Tasks;
using HRAssistant.Web.Admin.Contracts.VacancyContracts;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;
using Microsoft.AspNetCore.Mvc;

namespace HRAssistant.Web.Controllers
{
    [Route("api/[controller]")]
    public class VacancyController : Controller
    {
        private readonly IBus _bus;

        public VacancyController(IBus bus)
        {
            Guard.AgainstNullArgument(nameof(bus), bus);

            _bus = bus;
        }

        [HttpPost]
        public Task<CreateVacancyResult> Create([FromBody] CreateVacancy request) => _bus.Request(request);
    }
}
