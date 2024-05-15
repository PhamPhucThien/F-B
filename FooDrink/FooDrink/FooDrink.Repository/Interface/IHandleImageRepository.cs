using FooDrink.DTO.Request.Image;
using FooDrink.DTO.Response.Image;
using System.Threading.Tasks;

namespace FooDrink.Repository.Interface
{
    public interface IHandleImageRepository
    {
        Task<UploadImageResponse> UploadImagesAsync(UploadImageRequest request, string webRootPath);
        string GetImageUrl(string rootPath, string entityType, Guid entityId, string fileName);
        Task<List<string>> GetEntityImageListAsync(string rootPath, string entityType, Guid entityId);
    }
}
