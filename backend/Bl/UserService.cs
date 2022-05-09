using Abstractions;
using Domain;
using Infrastructure;

namespace Bl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProvinceService _provinceService;
        private readonly IPasswordService _passwordService;

        public UserService(
            IUserRepository userRepository,
            IProvinceService provinceService,
            IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _provinceService = provinceService;
            _passwordService = passwordService;
        }

        public async Task<bool> RegisterAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (await _userRepository.ExistsAsync(user.Email))
            {
                throw new UserFriendlyException($"'{user.Email}' is already registered.");
            }

            if (!user.ProvinceId.HasValue 
                && await _provinceService.HasProvincesAsync(user.CountryId))
            {
                throw new UserFriendlyException("Please specify Province field.");
            }

            if (user.ProvinceId.HasValue
                && !(await _provinceService.ExistsAsync(user.CountryId, user.ProvinceId.Value)))
            {
                throw new UserFriendlyException("Specified Province does not belong provided Country.");
            }

            user.Password = _passwordService.CalculateHash(user.Password);

            var result = await _userRepository.RegisterAsync(user);

            return result > 0;
        }

    }
}