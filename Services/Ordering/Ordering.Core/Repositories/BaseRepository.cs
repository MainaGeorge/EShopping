using Ordering.Core.Entities;
using System.Linq.Expressions;

namespace Ordering.Core.Repositories
{
    public interface IBaseRepository<T> where T : AuditEntry
    {
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
