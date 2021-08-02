using System;
using Microsoft.AspNetCore.Http;
using Shop.Business.ModelsDto;

namespace Shop.Business.Models
{
    public class StuffModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PlatformTypeEnum Platform { get; set; }
        public DateTime DateCreated { get; set; }
        public float TotalRating { get; set; }
        public string Genre { get; set; }
        public int Rating { get; set; }
        public IFormFile Logo { get; set; }
        public IFormFile Background { get; set; }
        public float Price { get; set; }
        public int Count { get; set; }
    }
}
