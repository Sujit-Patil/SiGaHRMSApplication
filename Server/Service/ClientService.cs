using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Service;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private ILogger<ClientService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="IClientRepository">dfhgdj</param>
    /// <param name="ILogger<ClientService>">gfhk</param>
    public ClientService(IClientRepository clientRepository, ILogger<ClientService> logger)
    {
        _clientRepository = clientRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddClientAsync(Client client)
    {

        await _clientRepository.AddAsync(client);
        await _clientRepository.CompleteAsync();
        _logger.LogInformation($"[AddClientAsyns] - {client.ClientId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateClientAsync(Client client)
    {
        await _clientRepository.UpdateAsync(client);
        await _clientRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateClientAsyns] - Client updated successfully for the {client.ClientId}");
    }

    /// <inheritdoc/>
    public async Task<Client?> GetClientByIdAsync(int id)
    {
        return await _clientRepository.
            FirstOrDefaultAsync(x => x.ClientId == id);
    }

    /// <inheritdoc/>
    public List<Client> GetAllClients()
    {
        var clientList = _clientRepository.GetAll();
        return (List<Client>)clientList;
    }

    /// <inheritdoc/>
    public async Task DeleteClientAsync(int clientId)
    {
        await _clientRepository.DeleteAsync(x => x.ClientId == clientId);
        await _clientRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteClientAsync] - Client deleted successfully for the {clientId}");
    }

}
