using System.Threading.Tasks;
using HRAssistant.DataAccess.Core;
using HRAssistant.Infrastructure.CQRS;
using HRAssistant.Web.Admin.Contracts.CityContracts;
using HRAssistant.Web.DataAccess.Core;
using LiteGuard;

namespace HRAssistant.Web.Admin.UseCases
{
    internal sealed class UpdateCityHandler : ICommandHandler<UpdateCity>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCityHandler(ICityRepository cityRepository, IUnitOfWork unitOfWork)
        {
            Guard.AgainstNullArgument(nameof(cityRepository), cityRepository);
            Guard.AgainstNullArgument(nameof(unitOfWork), unitOfWork);

            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateCity request)
        {
            var city = request.City;

            var entity = await _cityRepository.Get(city.Id.Value);
            entity.Name = city.Name;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
