using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Constants;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Validations;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>
/// Client Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    private ILogger<ClientController> _logger;
    private ValidationResult validationResult;


    /// <summary>
    /// Initializes a new instance of see ref<paramref name="ClientController"/>
    /// </summary>
    /// <param name="clientService"></param>
    public ClientController(IClientService clientService, ILogger<ClientController> logger)
    {
        _clientService = clientService;
        validationResult = new ValidationResult();
        _logger = logger;
    }

    /// <summary>
    /// The controller method to retrive all Clients.
    /// </summary>
    /// <returns>returns list of Clients</returns>

    [HttpGet]
    [Authorize(Roles = RoleConstants.SUPERADMIN + "," + RoleConstants.HR)]
    public Task<IEnumerable<Client>> GetAllClients()
    {
        return _clientService.GetAllClients();
    }

    /// <summary>
    /// Get method to retrive single Client
    /// </summary>
    /// <param name="id">Client Id</param>
    /// <returns> return single Client using Client Id</returns>
    [HttpGet("{id:int}")]
    public async Task<Client?> GetClientByIdAsync(int id)
    {
        return await _clientService.GetClientByIdAsync(id);
    }

    /// <summary>
    /// Post method to Add Client to database
    /// </summary>
    /// <param name="client"> Client object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPost]
    public async Task<IActionResult> AddClientAsync(Client client)
    {   
        try
        {
            await _clientService.AddClientAsync(client);

            return Ok(validationResult);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[AddClientAsync] Error Occurs : {ex.Message}");
            validationResult.AddErrorMesageCode(UserActionConstants.UnExpectedException, UserActionConstants.ErrorDescriptions);
            return BadRequest(validationResult);
        }
    }

    /// <summary>
    /// Upadte method to Update Client to database
    /// </summary>
    /// <param name="client">Client object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateClientAsync(Client client)
    {
        try
        {
            await _clientService.UpdateClientAsync(client);

            return Ok(validationResult);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[UpdateClientAsync] Error Occurs : {ex.Message}");
            validationResult.AddErrorMesageCode(UserActionConstants.UnExpectedException, UserActionConstants.ErrorDescriptions);
            return BadRequest(validationResult);
        }
    }

    /// <summary>
    /// Delete method to delete Client to database
    /// </summary>
    /// <param name="id">Client Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteClientAsync(int id)
    {
        await _clientService.DeleteClientAsync(id);
        return true;
    }
}
