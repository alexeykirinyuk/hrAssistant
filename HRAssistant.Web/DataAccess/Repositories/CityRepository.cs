using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRAssistant.DataAccess;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using LiteGuard;

namespace HRAssistant.Web.DataAccess.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly DatabaseContext _context;

        public CityRepository(DatabaseContext context)
        {
            Guard.AgainstNullArgument(nameof(context), context);

            _context = context;
        }

        public async Task<CityEntity> Get(Guid cityId)
        {
            var city = await _context.Cities.FindAsync(cityId);
            if (city == null)
            {
                throw new InvalidOperationException($"City with id '{cityId}' was not found.");
            }

            return city;
        }

        public IQueryable<CityEntity> Search()
        {
            throw new NotImplementedException();
        }

        public void Add(CityEntity city)
        {
            throw new NotImplementedException();
        }
    }
}
