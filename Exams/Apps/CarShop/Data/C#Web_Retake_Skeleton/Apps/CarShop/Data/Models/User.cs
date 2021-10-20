namespace CarShop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [MaxLength(20)]
        public string Username  { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password{ get; set; }
        public bool IsMechanic { get; set; }
    }
}
