using System.Threading.Tasks;
using HRAssistant.Web.Contracts.InterviewWorkflow;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.UseCases.InterviewWorkflow
{
    internal sealed class GetVacancyHandler : IQueryHandler<GetVacancy, GetVacancyResult>
    {
        private readonly IVacancyRepository _vacancyRepository;

        public GetVacancyHandler(IVacancyRepository vacancyRepository)
        {
            Guard.AgainstNullArgument(nameof(vacancyRepository), vacancyRepository);

            _vacancyRepository = vacancyRepository;
        }

        public async Task<GetVacancyResult> Handle(GetVacancy query)
        {
            var vacancy = await _vacancyRepository.Get(query.VacancyId.Value);

            return new GetVacancyResult
            {
                Title = vacancy.JobPosition.Title,
                Description = vacancy.Form.Description,
                QuestionsCount = vacancy.Form.Questions.Count
            };
        }
    }
}
