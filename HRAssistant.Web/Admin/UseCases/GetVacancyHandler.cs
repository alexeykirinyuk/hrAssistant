using System;
using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.Admin.Contracts.VacancyContracts;
using HRAssistant.Web.Admin.UseCases.Mapping;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.Admin.UseCases
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
