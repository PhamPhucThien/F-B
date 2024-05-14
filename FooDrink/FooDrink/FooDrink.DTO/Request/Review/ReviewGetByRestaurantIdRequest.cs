using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.DTO.Request.Review
{
    public class ReviewGetByRestaurantIdRequest
    {
        public Guid RestaurantId { get; set; }
    }
}
