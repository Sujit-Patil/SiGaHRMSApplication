using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>
/// User Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="UserController"/>
    /// </summary>
    /// <param name="userService"></param>
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// The controller method to retrive all Users.
    /// </summary>
    /// <returns>returns list of Users</returns>
    
    [HttpGet]
    [Authorize(Roles ="User")]
    public List<User> GetAllUsers()
    {
        return _userService.GetAllUsers();
    }

    /// <summary>
    /// Get method to retrive single User
    /// </summary>
    /// <param name="id">User Id</param>
    /// <returns> return single User using User Id</returns>
    [HttpGet("{id:int}")]
    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userService.GetUserByIdAsync(id);
    }

    /// <summary>
    /// Post method to Add User to database
    /// </summary>
    /// <param name="User"> User object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPost]
    public async Task AddUserAsync(User user)
    {
        await _userService.AddUserAsync(user);
    }

    /// <summary>
    /// Upadte method to Update User to database
    /// </summary>
    /// <param name="User">User object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task UpdateUserAsync(User user)
    {
        await _userService.UpdateUserAsync(user);
    }

    /// <summary>
    /// Delete method to delete User to database
    /// </summary>
    /// <param name="id">User Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteUserAsync(int id)
    {
        await _userService.DeleteUserAsync(id);
        return true;
    }
}
