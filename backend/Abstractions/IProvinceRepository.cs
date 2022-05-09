using Domain;

namespace Abstractions
{
    public interface IProvinceRepository
    {
        Task<List<Province>> GetByCountryAsync(int countryId);

        Task<bool> HasAnyByCountryAsync(int countryId);

        Task<bool> ExistsAsync(int countryId, int provinceId);
    }
}