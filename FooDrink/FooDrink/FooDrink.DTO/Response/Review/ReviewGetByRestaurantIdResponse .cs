﻿namespace FooDrink.DTO.Response.Review
{
    public class ReviewGetByRestaurantIdResponse
    {
        public List<ReviewResponse> Data { get; set; } = new List<ReviewResponse>();
    }
}
