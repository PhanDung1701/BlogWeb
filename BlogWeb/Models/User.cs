using System;
using System.Collections.Generic;

namespace BlogWeb.Models
{
    public partial class User
    {
        public User()
        {
            Posts = new HashSet<Post>();
        }

        public int UserId { get; set; }
        public string? Username { get; set; }
        public string PasswordHash { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
