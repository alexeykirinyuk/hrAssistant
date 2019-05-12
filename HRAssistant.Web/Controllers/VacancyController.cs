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

        [HttpGet]
        public Task<SearchVacanciesResult> Search([FromQuery] SearchVacancies request) => _bus.Execute(request);

        [HttpPost]
        public Task<CreateVacancyResult> Create([FromBody] CreateVacancy request) => _bus.Request(request);

        [HttpGet("{VacancyId}")]
        public Task<GetVacancyResult> Get([FromRoute] GetVacancy request) => _bus.Execute(request);

        [HttpPut]
        public Task Update([FromBody] UpdateVacancy request) => _bus.Request(request);

        [HttpPost("/open/{VacancyId}")]
        public Task Open([FromRoute] OpenVacancy request) => _bus.Request(request);

        [HttpPost("/close/{VacancyId}")]
        public Task Close([FromRoute] CloseVacancy request) => _bus.Request(request);
    }
}
