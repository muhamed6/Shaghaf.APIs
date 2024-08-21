using Microsoft.AspNetCore.Identity;
using Shaghaf.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Infrastructure.Identity
{
    public static class ApplicationIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    Email = "MohamedAshraf@gmail.com",
                    UserName = "MohamedAshraf",
                    PhoneNumber = "01282877366",
                };
                var result = await userManager.CreateAsync(user, "P@ssw0rd");
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }

                // Create the Admin role if it does not exist
                string roleName = "Admin";
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }

                // Assign the user to the Admin role
                await userManager.AddToRoleAsync(user, roleName);

                //await userManager.CreateAsync(user, "P@ssw0rd");

                
            }
        }
    }
}
