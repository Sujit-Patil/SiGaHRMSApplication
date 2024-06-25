using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// Designation service interface to perform Add, Update, and delete funtionality.
/// </summary>
/// <param name="Designation"></param>
public interface IDesignationService
{
    /// <summary>
    /// AddDesignationAsync method will add Designation to Db
    /// </summary>
    /// <param name="designation">Designation</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task AddDesignationAsync(Designation designation);

    /// <summary>
    /// UpdateDesignationAsync method perform update funtionality
    /// </summary>
    /// <param name="designation">Designation</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateDesignationAsync(Designation designation);

    /// <summary>
    /// DeleteDesignationAsync method perform Delete funtionality
    /// </summary>
    /// <param name="designationid">Designation Id</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteDesignationAsync(int designationId);

    /// <summary>
    /// GetDesignationByIdAsync method gives Designation using Id
    /// </summary>
    /// <param name="id">Designation Id</param>
    /// <returns>Returns single Designation </returns>
    public Task<Designation?> GetDesignationByIdAsync(int id);

    /// <summary>
    /// GetAllDesignations gives list of Designations
    /// </summary>
    /// <returns>Returns list of Designation</returns>
    public List<Designation> GetAllDesignations();

}
