using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.Admin.Contracts.CityContracts;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.Admin.UseCases
{
    internal sealed class GetCityHandler : IQueryHandler<GetCity, GetCityResult>
    {
        private readonly ICityRepository _cityRepository;

        public GetCityHandler(ICityRepository cityRepository)
        {
            Guard.AgainstNullArgument(nameof(cityRepository), cityRepository);

            _cityRepository = cityRepository;
        }

        public async Task<GetCityResult> Handle(GetCity query)
        {
            var entity = await _cityRepository.Get(query.CityId.Value);

            return new GetCityResult
            {
                City = new City
                {
                    Id = entity.Id,
                    Name = entity.Name
                }
            };
        }
    }
}
