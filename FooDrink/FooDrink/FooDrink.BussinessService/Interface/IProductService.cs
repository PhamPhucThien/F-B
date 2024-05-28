using FooDrink.DTO.Request;
using FooDrink.DTO.Response.Product;

namespace FooDrink.BussinessService.Interface
{
    public interface IProductService
    {
        IEnumerable<ProductGetListResponse> GetApplicationProductList(IPagingRequest pagingRequest);
    }
}
