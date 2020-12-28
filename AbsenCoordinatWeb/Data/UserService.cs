using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AbsenCoordinatWeb.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace AbsenCoordinatWeb.Data
{

    public class UserService 
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;
        private readonly UserManager<IdentityUser> _usermanagar;

        public UserService(IOptions<AppSettings> appSettings,   
            UserManager<IdentityUser> userManagar, 
            ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
            _appSettings = appSettings.Value;
            _usermanagar = userManagar;
        }

        public async Task<AuthenticateResponse> Authenticate(UserLogin model)
        {
            try
            {

                var user = await _usermanagar.FindByNameAsync(model.UserName.ToUpper());
                if (user != null && await _usermanagar.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await _usermanagar.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));

                    var token = new JwtSecurityToken(
                        expires: DateTime.Now.AddDays(7),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );
                    return new AuthenticateResponse(user, userRoles, new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);

                }
                throw new UnauthorizedAccessException();
            }
            catch (System.Exception ex)
            {
                throw new UnauthorizedAccessException(ex.Message);
            }
        }

        internal async Task<Karyawan> GetProfile(string username)
        {
            try
            {
                var user = await _usermanagar.FindByNameAsync(username.ToUpper());
                if (user == null)
                    throw new UnauthorizedAccessException("Anda Tidak Memiliki Akses");

                var karyawan = _context.Karyawans.SingleOrDefault(x => x.UserId == user.Id);
                if (karyawan == null)
                    throw new UnauthorizedAccessException("Anda Tidak Memiliki Profile");

                return karyawan;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }
    }
}