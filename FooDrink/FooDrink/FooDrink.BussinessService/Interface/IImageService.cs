using FooDrink.DTO.Request.Image;
using FooDrink.DTO.Response.Image;

namespace FooDrink.BussinessService.Interface
{
    public interface IImageService
    {
        Task<UploadImageResponse> UploadImagesAsync(UploadImageRequest request, string webRootPath);
        Task<List<string>> GetEntityImageListAsync(string rootPath, string entityType, Guid entityId);
    }
}
