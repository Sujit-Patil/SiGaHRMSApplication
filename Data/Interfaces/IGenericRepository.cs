using System.Linq.Expressions;

namespace SiGaHRMS.Data.Interfaces;

/// <summary>
/// Generic repository to Add, Update, Delete, Read fuctionality.
/// </summary>
public interface IGenericRepository<T> where T : class
{
    /// <summary>
    /// Add Entity.
    /// </summary>
    /// <param name="toAdd"></param>
    /// <returns>Returns asynchronous Task.</returns>
    Task AddAsync(T toAdd);

    /// <summary>
    /// Update Entity.
    /// </summary>
    /// <param name="toUpdate"></param>
    /// <returns>return Generic class object as asynchronous Task.</returns>
    public Task<T> UpdateAsync(T toUpdate);

    /// <summary>
    /// Delete Entity
    /// </summary>
    /// <param name="filter">filter</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteAsync(Expression<Func<T, bool>> filter);

    /// <summary>
    /// Get Entity.
    /// </summary>
    /// <param name="filter">filter</param>
    /// <returns>return Generic class object</returns>
    Task<T> GetAsync(Expression<Func<T, bool>> filter);

    /// <summary>
    /// Get all items from expression and include data from the Dbcontext.
    /// </summary>
    /// <returns>return Generic class object</returns>
    IEnumerable<T> GetAll
        (string? includeProperties = null);

    /// <summary>
    /// SaveChanges Entity
    /// </summary>
    /// <returns>return Generic integer</returns>
    Task<int> CompleteAsync();

    /// <summary>
    /// Get Entity from expression and include data from the Dbcontext
    /// </summary>
    /// <param name="filter">filter</param>
    /// <returns>return Generic class object</returns>
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter);
}
