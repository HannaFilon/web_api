﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shop.DAL.Core.Entities
{
    [Index(nameof(Name), IsUnique = false)]
    [Index(nameof(Platform), IsUnique = false)]
    [Index(nameof(DateCreated), IsUnique = false)]
    [Index(nameof(TotalRating), IsUnique = false)]
    [Index(nameof(Genre), IsUnique = false)]
    [Index(nameof(AgeRating), IsUnique = false)]
    [Index(nameof(Price), IsUnique = false)]
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

        public ICollection<ProductRating> Ratings { get; set; }

        [Range(0, 5, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public float TotalRating { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public string Genre { get; set; }

        [Range(0, 6, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public int AgeRating { get; set; }

        public string Logo { get; set; }

        public string Background { get; set; }

        public float Price { get; set; }

        public int Count { get; set; }
    }
}