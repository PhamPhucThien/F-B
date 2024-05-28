using Microsoft.AspNetCore.Http;

namespace FooDrink.DTO.Request.Image
{
    public class UploadImageRequest
    {
        public Guid EntityId { get; set; }
        public string EntityType { get; set; } = string.Empty;
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
    }
}
