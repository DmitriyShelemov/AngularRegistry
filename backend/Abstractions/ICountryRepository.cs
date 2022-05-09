using Domain;

namespace Abstractions
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetAllAsync();
    }
}