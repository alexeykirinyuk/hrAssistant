using System;
using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.CityManagement
{
    public sealed class GetCity : IQuery<GetCityResult>
    {
        public Guid? CityId { get;set; }
    }
}
