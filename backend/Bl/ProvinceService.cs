using Abstractions;
using Domain;

namespace Bl
{
    public class ProvinceService : IProvinceService
    {
        private readonly IProvinceRepository _repository;

        public ProvinceService(IProvinceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Province>> GetProvincesAsync(int countryId)
        {
            if (countryId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(countryId));
            }

            return await _repository.GetByCountryAsync(countryId);
        }

        public async Task<bool> HasProvincesAsync(int countryId)
        {
            if (countryId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(countryId));
            }

            return await _repository.HasAnyByCountryAsync(countryId);
        }

        public async Task<bool> ExistsAsync(int countryId, int provinceId)
        {
            if (countryId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(countryId));
            }

            if (provinceId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(provinceId));
            }

            return await _repository.ExistsAsync(countryId, provinceId);
        }
    }
}
