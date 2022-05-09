using Abstractions;
using Domain;

namespace Bl
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _repository;

        public CountryService(ICountryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
