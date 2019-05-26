using System.Threading.Tasks;
using HRAssistant.Web.Contracts.InterviewList;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;
using Microsoft.AspNetCore.Mvc;

namespace HRAssistant.Web.Controllers
{
    [Route("api/[controller]")]
    public sealed class InterviewListController : Controller
    {
        private readonly IBus _bus;

        public InterviewListController(IBus bus)
        {
            Guard.AgainstNullArgument(nameof(bus), bus);

            _bus = bus;
        }

        [HttpGet]
        public Task<SearchInterviewsResult> Search([FromQuery] SearchInterviews request) => _bus.Execute(request);

        [HttpGet("{InterviewId}")]
        public Task<GetInterviewResult> Get([FromRoute] GetInterview request) => _bus.Execute(request);
    }
}
