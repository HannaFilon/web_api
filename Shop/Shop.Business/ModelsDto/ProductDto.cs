using System;
using System.Text.Json.Serialization;

namespace Shop.Business.ModelsDto
{
    public class ProductDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PlatformTypeEnum Platform { get; set; }
        public DateTime DateCreated { get; set; }
        public float TotalRating { get; set; }
        public string Genre { get; set; }
        public int Rating { get; set; }
        public string Logo { get; set; }
        public string Background { get; set; }
        public float Price { get; set; }
        public int Count { get; set; }
    }
}
