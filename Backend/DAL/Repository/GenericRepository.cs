using Core.Abstract;
using DAL.Abstract;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DAL.Context;

namespace DAL.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly APIContext _context;

        public GenericRepository(APIContext commerceContext)
        {
            _context = commerceContext;
        }

        public async Task AddAsync(T actor)
        {
            await _context.Set<T>().AddAsync(actor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(c => c.ID == id);
            EntityEntry entry = _context.Entry<T>(entity);
            entry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _context.Set<T>().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] predicate)
        {
            IQueryable<T> query = _context.Set<T>();
            query = predicate.Aggregate(query, (current, includePropery) => current.Include(includePropery));
            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            var result = await _context.Set<T>().FirstOrDefaultAsync(c => c.ID == id);
            return result;
        }

        public async Task UpdateAsync(Guid id, T actor)
        {
            EntityEntry entry = _context.Entry<T>(actor);
            entry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
