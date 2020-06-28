using AnyTask.API.Data.Entities;
using AnyTask.API.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnyTask.API.Data.Interfaces
{
    public interface ITaskRepository : IRepositoryBase<Task>
    {
        void SetAsConcluded(int taskId);
    }
}
