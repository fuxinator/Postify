using Postify.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Postify.Data.ViewModels
{
    public class PostViewModel
    {
        public string Owner { get; set; }
        public string Content { get; set; }
        public string PostDate { get; set; }
        public int LikeCount { get; set; }
        public int Id { get; set; }
        public bool IsLikeable { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}
