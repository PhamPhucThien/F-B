using FooDrink.BussinessService.Interface;
using FooDrink.Database.Models;
using FooDrink.DTO.Request;
using FooDrink.DTO.Response.Product;
using FooDrink.Repository.Interface;

namespace FooDrink.BussinessService.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<ProductGetListResponse> GetApplicationProductList(IPagingRequest pagingRequest)
        {
            IEnumerable<Product> products = _repository.GetWithPaging(pagingRequest);

            IEnumerable<ProductGetListResponse> productListResponse = products.Select(p => new ProductGetListResponse
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price.ToString(),
                CategoryList = p.CategoryList,
                MenuId = p.MenuId
            });

            return productListResponse;
        }

    }
}
