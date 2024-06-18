using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// User service interface to perform Add, Update, and delete funtionality.
/// </summary>
/// <param name="user"></param>
public interface IUserService
{
    /// <summary>
    /// AddUserAsync method will add User to Db
    /// </summary>
    /// <param name="user">User</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task AddUserAsync(User user);

    /// <summary>
    /// UpdateUserAsync method perform update funtionality
    /// </summary>
    /// <param name="user">User</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateUserAsync(User user);

    /// <summary>
    /// DeleteUserAsync method perform Delete funtionality
    /// </summary>
    /// <param name="userid">User Id</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteUserAsync(int userId);

    /// <summary>
    /// GetUserByIdAsync method gives User using Id
    /// </summary>
    /// <param name="id">User Id</param>
    /// <returns>Returns single User </returns>
    public Task<User?> GetUserByIdAsync(int id);

    /// <summary>
    /// GetAllUsers gives list of users
    /// </summary>
    /// <returns>Returns list of user</returns>
    public List<User> GetAllUsers();

}
