using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Requests
{
    public class Login
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
}
