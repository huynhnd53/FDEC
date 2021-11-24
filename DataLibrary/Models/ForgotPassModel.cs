using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    class ForgotPassModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
}
