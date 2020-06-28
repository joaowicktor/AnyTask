using AnyTask.API.Data.Context;
using AnyTask.API.Data.Entities;
using AnyTask.API.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnyTask.API.Data.Repositories
{
    public class TaskRepository : RepositoryBase<Task>, ITaskRepository
    {
        public TaskRepository(AnyTaskDbContext context) : base(context) { }

        public async void SetAsConcluded(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
                task.Conclude();
        }
    }
}
