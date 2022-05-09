using Domain;

namespace Abstractions
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(User user);
    }
}