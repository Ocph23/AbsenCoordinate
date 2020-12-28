using System.Threading.Tasks;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using AbsenCoordinatWeb.Data;

namespace AbsenCoordinatWeb
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, 
            RoleManager<IdentityRole> roleManager, 
            UserManager<IdentityUser> userManagaer)
        {
            var trans = context.Database.BeginTransaction();
            try
            {
                if (!context.Roles.Any())
                {

                  await  roleManager.CreateAsync(new IdentityRole { Name = "Administrator" });
                  await  roleManager.CreateAsync(new IdentityRole { Name = "Hrd" });
                  await roleManager.CreateAsync(new IdentityRole { Name = "Karyawan" });
                }

                if(!context.Users.Any())
                {
                    var user = new IdentityUser { Email = "ocph23@gmail.com", UserName = "ocph23@gmail.com", EmailConfirmed=true };
                    await userManagaer.CreateAsync(user, "Sony@77");
                    await userManagaer.AddToRoleAsync(user, "Hrd");
                }
                trans.Commit();
            }
            catch (System.Exception ex)
            {
                trans.Rollback();
                throw new System.SystemException(ex.Message);
            }

        }
    }
}