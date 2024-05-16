using FooDrink.BussinessService.Interface;
using FooDrink.DTO.Request.Image;
using FooDrink.DTO.Response.Image;
using FooDrink.Repository.Interface;

namespace FooDrink.BussinessService.Service
{
    public class ImageService : IImageService
    {
        private readonly IHandleImageRepository _handleImageRepository;

        public ImageService(IHandleImageRepository handleImageRepository)
        {
            _handleImageRepository = handleImageRepository;
        }

        public async Task<UploadImageResponse> UploadImagesAsync(UploadImageRequest request, string webRootPath)
        {
            return await _handleImageRepository.UploadImagesAsync(request, webRootPath);
        }
        public async Task<List<string>> GetEntityImageListAsync(string rootPath, string entityType, Guid entityId)
        {
            return await _handleImageRepository.GetEntityImageListAsync(rootPath, entityType, entityId);
        }
    }
}
