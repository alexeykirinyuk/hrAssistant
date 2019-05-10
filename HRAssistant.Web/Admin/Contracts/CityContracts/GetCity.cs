using System;
using HRAssistant.Infrastructure.CQRS;

namespace HRAssistant.Web.Admin.Contracts.CityContracts
{
    public sealed class GetCity : IQuery<GetCityResult>
    {
        public Guid? CityId { get;set; }
    }
}
