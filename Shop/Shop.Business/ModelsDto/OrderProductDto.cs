using System;
using System.Text.Json.Serialization;

namespace Shop.Business.ModelsDto
{
    public class OrderProductDto
    {
        [JsonIgnore]
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
    }
}