using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.Contracts.InterviewList;
using HRAssistant.Web.Contracts.InterviewWorkflow;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using HRAssistant.Web.Infrastructure.CQRS;
using HRAssistant.Web.UseCases.InterviewWorkflow;
using LiteGuard;
using Question = HRAssistant.Web.Contracts.InterviewList.Question;

namespace HRAssistant.Web.UseCases.InterviewList
{
    internal sealed class GetInterviewHandler : IQueryHandler<GetInterview, GetInterviewResult>
    {
        private readonly IInterviewRepository _interviewRepository;

        public GetInterviewHandler(IInterviewRepository interviewRepository)
        {
            Guard.AgainstNullArgument(nameof(interviewRepository), interviewRepository);

            _interviewRepository = interviewRepository;
        }

        public async Task<GetInterviewResult> Handle(GetInterview query)
        {
            var interview = await _interviewRepository.Get(query.InterviewId.Value);

            return new GetInterviewResult
            {
                JobPositionTitle = interview.Vacancy.JobPosition.Title,
                TeamTitle = interview.Vacancy.Team.Title,
                FullName = interview.Candidate.FirstName + " " + interview.Candidate.LastName,
                CorrectAnswersCount = interview.Result.CorrectAnswersCount,
                IncorrectAnswersCount = interview.Result.IncorrectAnswersCount,
                Email = interview.Candidate.Email,
                CityTitle = interview.Vacancy.Team.City.Name,
                Phone = interview.Candidate.Phone,
                Questions = interview.GetQuestions()
                    .Select(q => new Question
                    {
                        Title = q.Question.Title,
                        Answer = q switch {
                            SelectQuestionSagaEntity select => string.Join(", ", select.SelectedOptions.Select(o => o.Title)),
                            InputQuestionSagaEntity input => input.Answer,
                            GeneralQuestionSagaEntity general => general.Answer,
                            _ => throw new InvalidOperationException($"Can't get answer for '{q.GetType().Name}' question.")
                            },
                        Result = q.Result
                    })
                    .ToArray()
            };
        }
    }
}