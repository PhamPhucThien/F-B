using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.DTO.Request.Restaurant
{
    public class ApproveRestaurantPartnerRequest
    {
        public Guid Id { get; set; }
        public bool IsRegistration { get; set; }
    }
}
