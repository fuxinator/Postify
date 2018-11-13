using System;
using System.Collections.Generic;
using System.Text;

namespace Postify.Data.ViewModels
{
    public class RegisterUserViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ConfirmPassword { get; set; }
        public string Description { get; set; }
        public string Class { get; set; }
    }
}
