using AnyTask.API.Data.Context;
using AnyTask.API.Data.Entities;
using AnyTask.API.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AnyTask.API.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AnyTaskDbContext context) : base(context) { }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserWithTasks(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Tasks)
                .SingleOrDefaultAsync(u => u.Id == userId);

            return user;
        }
    }
}
