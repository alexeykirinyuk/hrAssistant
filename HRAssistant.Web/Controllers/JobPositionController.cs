using System.Threading.Tasks;
using HRAssistant.Web.Admin.Contracts.JobPositionContracts;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;
using Microsoft.AspNetCore.Mvc;

namespace HRAssistant.Web.Controllers
{
    [Route("api/[controller]")]
    public class JobPositionController : Controller
    {
        private readonly IBus _bus;

        public JobPositionController(IBus bus)
        {
            Guard.AgainstNullArgument(nameof(bus), bus);

            _bus = bus;
        }

        [HttpGet]
        public async Task<SearchJobPositionsResult> Search([FromQuery] SearchJobPositions request) => await _bus.Execute(request);

        [HttpGet("{jobPositionId}")]
        public async Task<GetJobPositionResult> Get(GetJobPosition request) => await _bus.Execute(request);

        [HttpPost]
        public async Task<CreateJobPositionResult> Create([FromBody] CreateJobPosition request) => await _bus.Request(request);

        [HttpPut]
        public async Task Update([FromBody] UpdateJobPosition request) => await _bus.Request(request);
    }
}
