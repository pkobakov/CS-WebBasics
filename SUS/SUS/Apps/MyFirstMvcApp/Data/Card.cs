namespace BattleCards.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public class Card
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Keyword { get; set; }

        [Required]
        public int Attack { get; set; }

        [Required]
        public int Health { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        public virtual ICollection<UserCard> Users => new HashSet<UserCard>();

    }
}
