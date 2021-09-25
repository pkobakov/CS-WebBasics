namespace BattleCards.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


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
