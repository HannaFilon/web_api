using System;

namespace Shop.Business.ModelsDto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PlatformTypeEnum Platform { get; set; }
        public DateTime DateCreated { get; set; }

        public float TotalRating { get; set; }
    }
}
