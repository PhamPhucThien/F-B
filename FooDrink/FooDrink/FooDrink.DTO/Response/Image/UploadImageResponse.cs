using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.DTO.Response.Image
{
    public class UploadImageResponse
    {
        public bool Success { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
