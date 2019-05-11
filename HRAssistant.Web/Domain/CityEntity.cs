using System;
using System.Collections.Generic;

namespace HRAssistant.Web.Domain
{
    public sealed class CityEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<TeamEntity> Teams { get; set; }
    }
}
