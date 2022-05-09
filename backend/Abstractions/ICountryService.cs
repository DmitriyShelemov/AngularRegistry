using Domain;

namespace Abstractions
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetCountriesAsync();
    }
}