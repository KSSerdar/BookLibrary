using Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] predicate);
        Task<T> GetAsync(Guid id);
        Task AddAsync(T actor);
        Task UpdateAsync(Guid id, T actor);
        Task DeleteAsync(Guid id);
    }
}
