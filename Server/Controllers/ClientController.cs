using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>
/// Client Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="ClientController"/>
    /// </summary>
    /// <param name="clientService"></param>
    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    /// <summary>
    /// The controller method to retrive all Clients.
    /// </summary>
    /// <returns>returns list of Clients</returns>
    
    [HttpGet]
    public List<Client> GetAllClients()
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
    public async Task AddClientAsync(Client client)
    {
        await _clientService.AddClientAsync(client);
    }

    /// <summary>
    /// Upadte method to Update Client to database
    /// </summary>
    /// <param name="client">Client object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task UpdateClientAsync(Client client)
    {
        await _clientService.UpdateClientAsync(client);
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
