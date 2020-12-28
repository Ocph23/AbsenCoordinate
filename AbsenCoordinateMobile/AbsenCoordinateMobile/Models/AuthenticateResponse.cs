
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbsenCoordinateMobile
{
    public class AuthenticateResponse
    {
       

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime Expired { get; }
        public IEnumerable<string> Roles { get; set; }
    }
}