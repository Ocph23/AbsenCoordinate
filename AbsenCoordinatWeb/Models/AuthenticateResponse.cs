using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbsenCoordinatWeb
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse(){}

        public AuthenticateResponse(IdentityUser user, IList<string> userRoles, string token, DateTime validTo)
        {
            this.UserName = user.UserName;
            this.Email = user.Email;
            this.Token = token;
            this.Roles = userRoles;
            this.Expired = validTo;
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime Expired { get; }
        public IEnumerable<string> Roles { get; set; }
    }
}