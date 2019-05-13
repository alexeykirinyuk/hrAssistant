using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRAssistant.Web.Contracts.VacancyManagement;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using HRAssistant.Web.Infrastructure.CQRS;
using HRAssistant.Web.UseCases.Mapping;
using LiteGuard;

namespace HRAssistant.Web.UseCases.VacancyManagement
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
