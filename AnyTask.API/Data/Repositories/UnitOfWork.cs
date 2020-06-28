using AnyTask.API.Data.Context;
using AnyTask.API.Data.Entities;
using AnyTask.API.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnyTask.API.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private AnyTaskDbContext _context;
        private IUserRepository _userRepository;
        private ITaskRepository _taskRepository;

        public UnitOfWork(AnyTaskDbContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository {
            get {
                return _userRepository ??= new UserRepository(_context);
            }
        }

        public ITaskRepository TaskRepository {
            get {
                return _taskRepository ??= new TaskRepository(_context);
            }
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Rollback()
        {
            _context.Dispose();
        }
    }
}
