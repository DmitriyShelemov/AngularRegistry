using Abstractions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Dal
{
    public class UserRepository : IUserRepository
    {
        private readonly RegistryContext _context;

        public UserRepository(RegistryContext context)
        {
            _context = context;
        }

        public Task<List<User>> GetAllAsync()
        {
            return _context.Users.ToListAsync();
        }

        public Task<bool> ExistsAsync(string email)
        {
            return _context.Users.AnyAsync(x => string.Equals(x.Email, email));
        }

        public async Task<int> RegisterAsync(User user)
        {
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync(true);
        }
    }
}