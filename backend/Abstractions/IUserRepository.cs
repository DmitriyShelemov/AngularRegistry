using Domain;

namespace Abstractions
{
    public interface IUserRepository
    {
        Task<bool> ExistsAsync(string email);

        Task<List<User>> GetAllAsync();

        Task<int> RegisterAsync(User user);
    }
}