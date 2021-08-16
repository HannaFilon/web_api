using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shop.Business.ModelsDto
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
        public bool Completed { get; set; }
        public List<OrderProductDto> Products { get; set; } = new List<OrderProductDto>();
    }
}