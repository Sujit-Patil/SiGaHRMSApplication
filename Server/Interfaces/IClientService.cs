using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// Client service interface to perform Add, Update, and delete funtionality.
/// </summary>
/// <param name="Client"></param>
public interface IClientService
{
    /// <summary>
    /// AddClientAsync method will add Client to Db
    /// </summary>
    /// <param name="client">Client</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task AddClientAsync(Client client);

    /// <summary>
    /// UpdateClientAsync method perform update funtionality
    /// </summary>
    /// <param name="client">Client</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateClientAsync(Client client);

    /// <summary>
    /// DeleteClientAsync method perform Delete funtionality
    /// </summary>
    /// <param name="clientid">Client Id</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteClientAsync(int clientId);

    /// <summary>
    /// GetClientByIdAsync method gives Client using Id
    /// </summary>
    /// <param name="id">Client Id</param>
    /// <returns>Returns single Client </returns>
    public Task<Client?> GetClientByIdAsync(int id);

    /// <summary>
    /// GetAllClients gives list of Clients
    /// </summary>
    /// <returns>Returns list of Client</returns>
    public Task<IEnumerable<Client>> GetAllClients();

}
