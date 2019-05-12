using System.Threading.Tasks;
using HRAssistant.Web.Admin.Contracts.TeamContracts;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;
using Microsoft.AspNetCore.Mvc;

namespace HRAssistant.Web.Controllers
{
    [Route("api/[controller]")]
    public sealed class TeamController : Controller
    {
        private readonly IBus _bus;

        public TeamController(IBus bus)
        {
            Guard.AgainstNullArgument(nameof(bus), bus);

            _bus = bus;
        }

        [HttpGet]
        public Task<SearchTeamsResult> Search([FromQuery] SearchTeams request) => _bus.Execute(request);

        [HttpGet("{teamId}")]
        public Task<GetTeamResult> Get([FromRoute] GetTeam request) => _bus.Execute(request);

        [HttpPost]
        public Task<CreateTeamResult> Create([FromBody] CreateTeam request) => _bus.Request(request);

        [HttpPut]
        public Task Update([FromBody] UpdateTeam request) => _bus.Request(request);
    }
}
