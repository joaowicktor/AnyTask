using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AnyTask.API.Data.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<T> FindById(int id);
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
