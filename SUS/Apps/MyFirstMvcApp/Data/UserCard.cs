namespace BattleCards.Data
{
    using System.ComponentModel.DataAnnotations;


    public class UserCard
    {
        [Key]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        [Key]
        public int CardId { get; set; }
        public virtual Card Card { get; set; }
    }
}
