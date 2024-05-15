using FooDrink.DTO.Request.Image;
using FooDrink.DTO.Response.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.BussinessService.Interface
{
    public interface IImageService
    {
        Task<UploadImageResponse> UploadImagesAsync(UploadImageRequest request, string webRootPath);
    }
}
