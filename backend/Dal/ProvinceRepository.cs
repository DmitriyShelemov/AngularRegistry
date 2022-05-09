using Abstractions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Dal
{
    public class ProvinceRepository : IProvinceRepository
    {
        private readonly RegistryContext _context;

        public ProvinceRepository(RegistryContext context)
        {
            _context = context;
        }

        public Task<List<Province>> GetByCountryAsync(int countryId)
        {
            return _context.Provinces.Where(x => x.CountryId == countryId).ToListAsync();
        }

        public Task<bool> HasAnyByCountryAsync(int countryId)
        {
            return _context.Provinces.AnyAsync(x => x.CountryId == countryId);
        }

        public Task<bool> ExistsAsync(int countryId, int provinceId)
        {
            return _context.Provinces.AnyAsync(x => x.Id == provinceId && x.CountryId == countryId);
        }
    }
}