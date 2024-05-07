using FooDrink.DTO.Request;
using FooDrink.DTO.Response.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.BussinessService.Interface
{
    public interface IProductService
    {
       IEnumerable<ProductGetListResponse> GetApplicationProductList(IPagingRequest pagingRequest);
    }
}
