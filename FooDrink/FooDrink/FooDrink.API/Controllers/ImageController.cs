using FooDrink.BussinessService.Interface;
using FooDrink.DTO.Request.Image;
using Microsoft.AspNetCore.Mvc;

namespace FooDrink.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _env;

        public ImageController(IImageService imageService, IWebHostEnvironment env)
        {
            _imageService = imageService;
            _env = env;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImages([FromForm] UploadImageRequest request)
        {
            var response = await _imageService.UploadImagesAsync(request, _env.WebRootPath);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
