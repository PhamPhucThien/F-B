using FooDrink.BussinessService.Interface;
using FooDrink.DTO.Request.Image;
using FooDrink.DTO.Response.Image;
using FooDrink.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
