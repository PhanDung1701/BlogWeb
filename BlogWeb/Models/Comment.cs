﻿using System;
using System.Collections.Generic;

namespace BlogWeb.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int? PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }
}
