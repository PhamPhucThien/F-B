using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.DTO.Response.Review
{
    public class ReviewGetByIdResponse
    {
        public List<ReviewResponse> Data { get; set; } = new List<ReviewResponse>();
    }
}
