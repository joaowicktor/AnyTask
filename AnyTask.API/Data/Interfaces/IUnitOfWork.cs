using System.Threading.Tasks;

namespace AnyTask.API.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ITaskRepository TaskRepository { get; }
        Task<int> CommitAsync();
        void Rollback();
    }
}
