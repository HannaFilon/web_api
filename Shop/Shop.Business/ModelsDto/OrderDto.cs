using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Shop.DAL.Core.Entities;

namespace Shop.Business.ModelsDto
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
        public bool Comleted { get; set; }
        public List<OrderProduct> Products { get; set; }
    }
}