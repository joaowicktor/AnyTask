using AnyTask.API.Data.Entities;
using AnyTask.API.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnyTask.API.Data.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> FindByEmailAsync(string email);
        Task<User> GetUserWithTasks(int userId);
    }
}
