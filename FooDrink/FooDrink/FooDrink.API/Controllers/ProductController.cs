using FooDrink.BussinessService.Service;
using FooDrink.DTO.Request.Product;
using FooDrink.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FooDrink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productService;

        public ProductController(IProductRepository productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        /*[HttpGet("GetListProduct")]
        public IActionResult GetListProduct([FromQuery] ProductGetListRequest request)
        {
            try
            {
                var productList = _productService.GetApplicationProductList(request);
                return Ok(productList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }*/
    }
}
