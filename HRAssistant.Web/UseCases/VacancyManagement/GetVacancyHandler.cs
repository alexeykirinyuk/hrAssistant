using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.Contracts.VacancyManagement;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Infrastructure.CQRS;
using HRAssistant.Web.UseCases.JobPositionManagement;
using HRAssistant.Web.UseCases.Mapping;
using LiteGuard;

namespace HRAssistant.Web.UseCases.VacancyManagement
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
            var entity = await _vacancyRepository.Get(query.VacancyId.Value);

            return new GetVacancyResult
            {
                Vacancy = new Vacancy
                {
                    Id = entity.Id,
                    CandidateRequirements = entity.CandidateRequirements,
                    JobPositionId = entity.JobPositionId,
                    JobsNumber = entity.JobsNumber,
                    Salary = entity.Salary,
                    TeamId = entity.TeamId,
                    Form = new Form
                    {
                        Description = entity.Form.Description,
                        Questions = entity.Form.Questions.Select(q => q.CreateContractQuestion())
                            .ToArray()
                    },
                    Status = entity.Status.CreateContract()
                }
            };
        }
    }
}
