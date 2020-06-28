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
    }
}
