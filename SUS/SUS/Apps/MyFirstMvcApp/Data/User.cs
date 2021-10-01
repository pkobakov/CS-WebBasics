namespace BattleCards.Data
{
    using SUS.MvcFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Role = IdentityRole.User;
        }


        public virtual ICollection<UserCard> Cards => new HashSet<UserCard>();  
    }
}
