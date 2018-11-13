using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Postify.Data.Models;

namespace Postify.Data.Models
{
    public class Comment
    {
        public Comment()
        {
            Likes = new List<CommentLike>();
        }

        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime Published { get; set; }
        public List<CommentLike> Likes { get; set; }
        
        public ApplicationUser User { get; set; }
    }
}
