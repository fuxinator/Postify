using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Positfy.Web.Models
{
    public class CreatePostViewModel
    {
        public string Content { get; set; }
        public int Index { get; set; }
    }
}
