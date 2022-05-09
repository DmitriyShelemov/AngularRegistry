using Abstractions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Dal
{
    public class CountryRepository : ICountryRepository
    {
        private readonly RegistryContext _context;

        public CountryRepository(RegistryContext context)
        {
            _context = context;

        }

        public Task<List<Country>> GetAllAsync()
        {
            return _context.Countries.ToListAsync();
        }
    }
}