using FooDrink.DTO.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.DTO.Response.Review
{
    public class ReviewGetListResponse
    {
        /// <summary>
        /// Page Size
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Page Index
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// Search string
        /// </summary>
        public string SearchString { get; set; } = string.Empty;

        /// <summary>
        /// List of User
        /// </summary>
        public List<ReviewResponse> Data { get; set; } = new List<ReviewResponse>();
    }
}
