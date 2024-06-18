using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private ILogger<UserService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="IUserRepository">dfhgdj</param>
    /// <param name="ILogger<UserService>">gfhk</param>
    public UserService(IUserRepository userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddUserAsync(User user)
    {

        await _userRepository.AddAsync(user);
        await _userRepository.CompleteAsync();
        _logger.LogInformation($"[AddUserAsyns] - {user.UserName} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateUserAsync(User user)
    {
        await _userRepository.UpdateAsync(user);
        await _userRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateUserAsyns] - User updated successfully for the {user.UserId}");
    }

    /// <inheritdoc/>
    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userRepository.
            FirstOrDefaultAsync(x => x.UserId == id);
    }

    /// <inheritdoc/>
    public List<User> GetAllUsers()
    {
        var userList = _userRepository.GetAll();
        return (List<User>)userList;
    }

    /// <inheritdoc/>
    public async Task DeleteUserAsync(int userId)
    {
        await _userRepository.DeleteAsync(x => x.UserId == userId);
        await _userRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteUserAsync] - User deleted successfully for the {userId}");
    }

}
