using System;
using System.Collections.Generic;
using System.Text;

namespace Postify.Data.ViewModels
{
    public class CreateCommentViewModel
    {
        public int PostId { get; set; }
        public string Content { get; set; }
    }
}
