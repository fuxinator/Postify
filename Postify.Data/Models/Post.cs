using Postify.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Postify.Data.Models
{
    public class Post : IComparable<Post>
    {
        public Post()
        {
            Comments = new List<Comment>();
            Likes = new List<PostLike>();
        }

        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime Published { get; set; }

        public List<Comment> Comments { get; set; }
        public List<PostLike> Likes { get; set; }
        public ApplicationUser User { get; set; }

        public int CompareTo(Post post)
        {
            return post.Published.CompareTo(Published);
        }
    }
}
