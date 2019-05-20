using HRAssistant.Web.Contracts.InterviewWorkflow;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;
using System;
using System.Threading.Tasks;

namespace HRAssistant.Web.UseCases.InterviewWorkflow
{
    internal sealed class GetInterviewStatusHandler : IQueryHandler<GetInterviewStatus, GetInterviewStatusResult>
    {
        private readonly IInterviewRepository _interviewRepository;

        public GetInterviewStatusHandler(IInterviewRepository interviewRepository)
        {
            Guard.AgainstNullArgument(nameof(interviewRepository), interviewRepository);

            _interviewRepository = interviewRepository;
        }

        public async Task<GetInterviewStatusResult> Handle(GetInterviewStatus query)
        {
            var status = await _interviewRepository.Get(query.InterviewId.Value);

            return new GetInterviewStatusResult
            {
                Status = InterviewStatus.Started
            };
        }
    }
}