using Microsoft.EntityFrameworkCore;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Service;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IAuditingService _auditingService;
    private ILogger<ClientService> _logger;

    /// <summary>
    /// Initializes a new instance of the ClientService class.
    /// </summary>
    /// <param name="clientRepository">The repository for managing client data.</param>
    /// <param name="logger">The logger for logging messages related to ClientService.</param>
    public ClientService(
        IClientRepository clientRepository,
        ILogger<ClientService> logger,
        IAuditingService auditingService)
    {
        _clientRepository = clientRepository;
        _logger = logger;
        _auditingService = auditingService;
    }


    /// <inheritdoc/>
    public async Task AddClientAsync(Client client)
    {
        client=_auditingService.SetAuditedEntity(client,true);
        await _clientRepository.AddAsync(client);
        await _clientRepository.CompleteAsync();
        _logger.LogInformation($"[AddClientAsyns] - {client.ClientId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateClientAsync(Client client)
    {
        client = _auditingService.SetAuditedEntity(client, false);
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
    public async Task<IEnumerable<Client>> GetAllClients()
    {
        return await _clientRepository.GetQueryable(x => x.IsDeleted == false).ToListAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteClientAsync(int clientId)
    {
        await _clientRepository.DeleteAsync(x => x.ClientId == clientId);
        await _clientRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteClientAsync] - Client deleted successfully for the {clientId}");
    }

}
