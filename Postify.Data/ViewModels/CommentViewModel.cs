using System;
using System.Collections.Generic;
using System.Text;

namespace Postify.Data.ViewModels
{
    public class CommentViewModel
    {
        public string User { get; set; }
        public string Content { get; set; }
        public string Published { get; set; }
        public int LikeCount { get; set; }
        public bool IsLikeable { get; set; }
    }
}
