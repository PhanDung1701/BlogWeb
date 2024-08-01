using System;
using System.Collections.Generic;

namespace BlogWeb.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        public int PostId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? Thumbnail { get; set; }
        public int? AuthorId { get; set; }
        public int? CategoryId { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsTrend { get; set; }

        public virtual User? Author { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
