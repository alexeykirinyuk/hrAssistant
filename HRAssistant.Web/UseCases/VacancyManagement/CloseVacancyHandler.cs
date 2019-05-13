using System.Threading.Tasks;
using HRAssistant.Web.Contracts.VacancyManagement;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.UseCases.VacancyManagement
{
    internal sealed class CloseVacancyHandler : ICommandHandler<CloseVacancy>
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CloseVacancyHandler(IVacancyRepository vacancyRepository, IUnitOfWork unitOfWork)
        {
            Guard.AgainstNullArgument(nameof(vacancyRepository), vacancyRepository);
            Guard.AgainstNullArgument(nameof(unitOfWork), unitOfWork);

            _vacancyRepository = vacancyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CloseVacancy request)
        {
            var vacancy = await _vacancyRepository.Get(request.VacancyId.Value);
            vacancy.Status = VacancyStatusEntity.Closed;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
