using System;
using System.Collections.Generic;
using System.Text;

namespace Postify.Data.Models
{
    public class PostLike
    {
        public int PostLikeId { get; set; }

        public ApplicationUser User { get; set; }

        public Post Post { get; set; }
    }
}
