using System;
using System.Threading.Tasks;
using HRAssistant.Web.Admin.Contracts.CityContracts;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.Admin.UseCases
{
    internal sealed class CreateCityHandler : ICommandHandler<CreateCity, CreateCityResult>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCityHandler(ICityRepository cityRepository, IUnitOfWork unitOfWork)
        {
            Guard.AgainstNullArgument(nameof(cityRepository), cityRepository);
            Guard.AgainstNullArgument(nameof(unitOfWork), unitOfWork);

            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateCityResult> Handle(CreateCity request)
        {
            var cityId = Guid.NewGuid();
            _cityRepository.Add(new CityEntity
            {
                Id = cityId,
                Name = request.City.Name
            });

            await _unitOfWork.SaveChangesAsync();

            return new CreateCityResult {CityId = cityId};
        }
    }
}
