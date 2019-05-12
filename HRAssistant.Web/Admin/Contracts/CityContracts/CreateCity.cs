using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Admin.Contracts.CityContracts
{
    public sealed class CreateCity : ICommand<CreateCityResult>
    {
        public City City { get; set; }
    }
}
