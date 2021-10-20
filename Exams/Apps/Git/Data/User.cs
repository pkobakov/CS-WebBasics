namespace Git.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual ICollection<Repository> Repositories { get; set; } = new HashSet<Repository>();
        public virtual ICollection<Commit> Commits { get; set; } = new HashSet<Commit>();
    }
}
