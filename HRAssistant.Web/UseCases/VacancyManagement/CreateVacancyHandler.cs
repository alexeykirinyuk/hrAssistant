using System;
using System.Collections.Generic;
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
    internal sealed class CreateVacancyHandler : ICommandHandler<CreateVacancy, CreateVacancyResult>
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateVacancyHandler(IVacancyRepository vacancyRepository, IUnitOfWork unitOfWork)
        {
            Guard.AgainstNullArgument(nameof(vacancyRepository), vacancyRepository);
            Guard.AgainstNullArgument(nameof(unitOfWork), unitOfWork);

            _vacancyRepository = vacancyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateVacancyResult> Handle(CreateVacancy command)
        {
            var vacancyId = Guid.NewGuid();
            var vacancy = command.Vacancy;

            var entity = new VacancyEntity
            {
                Id = vacancyId,
                Form = new FormEntity
                {
                    Questions = new List<QuestionEntity>()
                }
            };
            entity.Update(vacancy);
            _vacancyRepository.Add(entity);

            await _unitOfWork.SaveChangesAsync();

            return new CreateVacancyResult { VacancyId = vacancyId };
        }
    }
}
