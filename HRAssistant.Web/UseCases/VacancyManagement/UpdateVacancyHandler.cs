using System.Threading.Tasks;
using HRAssistant.Web.Admin.Contracts.VacancyContracts;
using HRAssistant.Web.Admin.UseCases.Mapping;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.Admin.UseCases
{
    internal sealed class UpdateVacancyHandler : ICommandHandler<UpdateVacancy>
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateVacancyHandler(
            IVacancyRepository vacancyRepository,
            IUnitOfWork unitOfWork)
        {
            Guard.AgainstNullArgument(nameof(vacancyRepository), vacancyRepository);
            Guard.AgainstNullArgument(nameof(unitOfWork), unitOfWork);

            _vacancyRepository = vacancyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateVacancy request)
        {
            var vacancy = await _vacancyRepository.Get(request.Vacancy.Id.Value);
            vacancy.Update(request.Vacancy);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
