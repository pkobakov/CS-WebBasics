namespace Git.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;



    public class Repository
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(10)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public string OwnerId { get; set; }
        public User Owner { get; set; }

        public virtual ICollection<Commit> Commits { get; set; } = new HashSet<Commit>();
    }
}
