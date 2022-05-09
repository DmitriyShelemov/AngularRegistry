using Domain;

namespace Abstractions
{
    public interface IProvinceService
    {
        Task<IEnumerable<Province>> GetProvincesAsync(int countryId);

        Task<bool> HasProvincesAsync(int countryId);

        Task<bool> ExistsAsync(int countryId, int provinceId);
    }
}