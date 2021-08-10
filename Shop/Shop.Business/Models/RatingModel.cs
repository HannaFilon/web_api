using System;

namespace Shop.Business.Models
{
    public class RatingModel
    {
        public Guid ProductId { get; set; }
        public float TotalRating { get; set; }
    }
}