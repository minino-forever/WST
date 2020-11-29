using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace WST.Admin.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        
        private const string adminPassword = "Secret123$";

        public static async Task EnsurePopulated(IApplicationBuilder app)
        {
            var userManager = app.ApplicationServices.GetRequiredService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(adminUser);

            if (user == null)
            {
                user = new IdentityUser("Admin");

                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}