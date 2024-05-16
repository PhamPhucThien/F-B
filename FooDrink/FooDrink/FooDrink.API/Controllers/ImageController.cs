using FooDrink.API.Configuration;
using FooDrink.BussinessService.Interface;
using FooDrink.DTO.Request.Image;
using FooDrink.DTO.Response.Image;
using Microsoft.AspNetCore.Mvc;

namespace FooDrink.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly ApppSettingConfig _appSettingConfig;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageController(IImageService imageService, IWebHostEnvironment webHostEnvironment, ApppSettingConfig apppSettingConfig)
        {
            _imageService = imageService;
            _webHostEnvironment = webHostEnvironment;
            _appSettingConfig = apppSettingConfig;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImages([FromForm] UploadImageRequest request)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            UploadImageResponse response = await _imageService.UploadImagesAsync(request, webRootPath);
            response.ImageUrls = response.ImageUrls.Select(i => i = _appSettingConfig.Domain + i).ToList();
            return response.Success ? Ok(response) : BadRequest(response.ErrorMessage);
        }

        [HttpGet("{entityType}/{entityId}/images")]
        public async Task<IActionResult> GetEntityImages(string entityType, Guid entityId)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            List<string> imageUrls = await _imageService.GetEntityImageListAsync(webRootPath, entityType, entityId);
            List<string> response = imageUrls.Select(i => _appSettingConfig.Domain + i).ToList();
            return Ok(response);
        }
    }
}
