using System.Threading.Tasks;
using HRAssistant.Web.Contracts.InterviewWorkflow;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;
using Microsoft.AspNetCore.Mvc;

namespace HRAssistant.Web.Controllers
{
    [Route("api/[controller]")]
    public sealed class InterviewWorkflowController : Controller
    {
        private readonly IBus _bus;

        public InterviewWorkflowController(IBus bus)
        {
            Guard.AgainstNullArgument(nameof(bus), bus);

            _bus = bus;
        }

        [HttpGet("vacancy/{VacancyId}")]
        public Task<GetVacancyResult> GetVacancy([FromRoute] GetVacancy request) => _bus.Execute(request);

        [HttpPost("contact")]
        public Task<SetContactInformationResult> SetContactInformation([FromBody] SetContactInformation request) => _bus.Request(request);

        [HttpPost("start")]
        public Task Start([FromBody] StartInterview request) => _bus.Request(request);

        [HttpPost("question")]
        public Task<StartQuestionResult> StartQuestion([FromBody] StartQuestion request) => _bus.Request(request);

        [HttpPost("answer")]
        public Task<AnswerResult> Answer([FromBody] Answer request) => _bus.Request(request);
    }
}
