using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SiGaHRMS.Data.Interfaces;
using System.Linq.Expressions;


namespace SiGaHRMS.Data.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DbContext _Context;
    public GenericRepository(DbContext Context)
    {
        _Context = Context;
    }

    public async Task AddAsync(T entity)
    {
        await _Context.Set<T>().AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _Context.Set<T>().Remove(entity);
    }

    public async Task DeleteAsync(Expression<Func<T, bool>> filter)
    {
        T entity = GetSingle(filter);
        _Context.Set<T>().Remove(entity);
    }

    public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter)
    {
        return _Context.Set<T>().FirstOrDefaultAsync(filter);
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
    {
        var query = GetQueryable(filter);
        return query.FirstOrDefault();
    }

    public IEnumerable<T> GetAll(string? includeProperties = null)
    {
        IQueryable<T> query = _Context.Set<T>();
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeprop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeprop);
            }
        }
        return query.ToList();
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _Context.Set<T>().Update(entity);
        return entity;
    }

    public Task<int> CompleteAsync()
    {
        return _Context.SaveChangesAsync();
    }

    private T GetSingle(Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        return GetQueryable(filter, include).FirstOrDefault();
    }

    private IQueryable<T> GetQueryable(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        IQueryable<T> query = _Context.Set<T>();

        if (include is not null)
        {
            query = include(query);
        }

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        return query;
    }

}
