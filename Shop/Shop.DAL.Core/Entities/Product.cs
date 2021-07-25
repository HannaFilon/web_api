using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shop.DAL.Core.Entities
{
    [Table("Products")]
    [Index(nameof(Name), IsUnique = false)]
    [Index(nameof(Platform), IsUnique = false)]
    [Index( nameof(DateCreated), IsUnique = false)]
    [Index(nameof(TotalRating), IsUnique = false)]
    [Index(nameof(Name), nameof(Platform), nameof(DateCreated),IsUnique = true)]
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Platform { get; set; }
        public DateTime DateCreated { get; set; }

        public float TotalRating { get; set; }

    }
}
