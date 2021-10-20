namespace CarShop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;


    public class Issue
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Description { get; set; }
        
        public bool IsFixed { get; set; }
        [Required]
        public string CarId { get; set; }
        public Car Car { get; set; }
    }
}
