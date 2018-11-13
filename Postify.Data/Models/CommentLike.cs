using System;
using System.Collections.Generic;
using System.Text;

namespace Postify.Data.Models
{
    public class CommentLike
    {
        public int CommentLikeId { get; set; }

        public ApplicationUser User { get; set; }

        public Comment Comment { get; set; }
    }
}
