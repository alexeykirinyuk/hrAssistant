using System.Threading.Tasks;
using HRAssistant.Web.Admin.Contracts.VacancyContracts;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.Admin.UseCases
{
    internal sealed class OpenVacancyHandler : ICommandHandler<OpenVacancy>
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OpenVacancyHandler(IVacancyRepository vacancyRepository, IUnitOfWork unitOfWork)
        {
            Guard.AgainstNullArgument(nameof(vacancyRepository), vacancyRepository);
            Guard.AgainstNullArgument(nameof(unitOfWork), unitOfWork);

            _vacancyRepository = vacancyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(OpenVacancy request)
        {
            var vacancy = await _vacancyRepository.Get(request.VacancyId.Value);
            vacancy.Status = VacancyStatusEntity.Opened;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
