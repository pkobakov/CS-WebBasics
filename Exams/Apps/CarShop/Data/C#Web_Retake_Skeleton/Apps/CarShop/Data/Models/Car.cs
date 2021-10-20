namespace CarShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Car
   {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [MaxLength(20)]
        public string Model { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [MaxLength(10)]
        public string PlateNumber { get; set; }
        [Required]
        public string OwnerId { get; set; }
        public User Owner { get; set; }
        public ICollection<Issue> Issues { get; set; } = new List<Issue>();
    }
}
