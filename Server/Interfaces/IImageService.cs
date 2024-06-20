namespace SiGaHRMS.ApiService.Interfaces;

public interface IImageService
{
    Task<string> SaveImageAsync(IFormFile file);
}
