namespace BattleCards.Data
{
    using SUS.MvcFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class User : UserIdentity
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }


        public virtual ICollection<UserCard> Cards => new HashSet<UserCard>();  
    }
}
