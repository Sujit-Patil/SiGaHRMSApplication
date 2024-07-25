using SiGaHRMS.ApiService.Interfaces;

namespace SiGaHRMS.ApiService.Service;

public class ImageService : IImageService
{

    private readonly string _imageUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile-images"); // Example: physical path to store images
    public ImageService()
    {


    }

    public async Task<string> SaveImageAsync(IFormFile file)
    {
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(_imageUploadPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return filePath;
    }

}
