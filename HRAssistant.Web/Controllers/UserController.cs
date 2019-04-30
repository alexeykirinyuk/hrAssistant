using System.Threading.Tasks;
using HRAssistant.Admin.Contracts;
using HRAssistant.Admin.Contracts.UserContracts;
using HRAssistant.Infrastructure.CQRS;
using LiteGuard;
using Microsoft.AspNetCore.Mvc;

namespace HRAssistant.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IBus _bus;

        public UserController(IBus mediator)
        {
            Guard.AgainstNullArgument(nameof(mediator), mediator);

            _bus = mediator;
        }

        [HttpGet]
        public async Task<SearchUsersResult> Search([FromQuery] SearchUsers request) => await _bus.Execute(request);

        [HttpGet("{userId}")]
        public async Task<GetUserResult> Get(GetUser request) => await _bus.Execute(request);

        [HttpPost]
        public async Task<AddUserResult> Add([FromBody] AddUser request) => await _bus.Request(request);

        [HttpPut]
        public async Task Update([FromBody] UpdateUser request) => await _bus.Request(request);
    }
}
