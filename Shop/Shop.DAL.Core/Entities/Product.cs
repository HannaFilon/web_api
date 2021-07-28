using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shop.DAL.Core.Entities
{
    [Index(nameof(Name), IsUnique = false)]
    [Index(nameof(Platform), IsUnique = false)]
    [Index(nameof(DateCreated), IsUnique = false)]
    [Index(nameof(TotalRating), IsUnique = false)]
    public class Product
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, 4, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public int Platform { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime DateCreated { get; set; }

        [Required]
        [Range(0, 5, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public float TotalRating { get; set; }

    }
}
