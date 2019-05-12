using System;
using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using LiteGuard;
using Microsoft.EntityFrameworkCore;

namespace HRAssistant.Web.DataAccess.Repositories
{
    internal sealed class CityRepository : ICityRepository
    {
        private readonly DatabaseContext _context;

        public CityRepository(DatabaseContext context)
        {
            Guard.AgainstNullArgument(nameof(context), context);

            _context = context;
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Cities.AnyAsync(city => city.Id == id);
        }

        public async Task<bool> Exists(string name, Guid? excludeCityId = null)
        {
            return await _context.Cities
                .AnyAsync(city => city.Name == name && (!excludeCityId.HasValue || city.Id != excludeCityId.Value));
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

        public IQueryable<CityEntity> Search() => _context.Cities;

        public void Add(CityEntity city) => _context.Cities.Add(city);
    }
}
