using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model.Entity;
using System.Linq.Expressions;


namespace SiGaHRMS.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);  // Hard delete
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> filter)
        {
            T entity = await GetSingleAsync(filter);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().FirstOrDefaultAsync(filter);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await GetQueryable(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await CompleteAsync();
            return entity;
        }

        public Task<int> CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }

        private async Task<T?> GetSingleAsync(Expression<Func<T, bool>>? filter = null,
                Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            return await GetQueryable(filter, include).FirstOrDefaultAsync();
        }

        public IQueryable<T> GetQueryable(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (include != null)
            {
                query = include(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }
    }
}
