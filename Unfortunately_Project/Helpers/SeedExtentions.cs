using Microsoft.AspNetCore.Identity;
using Unfortunately_Project.Models;

namespace Unfortunately_Project.Helpers
{
    public static class SeedExtensions
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in Enum.GetNames(typeof(Roles)))
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task SeedAdmin(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            const string EMAIL = "MainAdmin12@gmail.com";
            const string PASSWORD = "Admin4ik@";

            var existingUser = await userManager.FindByEmailAsync(EMAIL);

            if (existingUser == null)
            {
                var user = new IdentityUser
                {
                    UserName = EMAIL,
                    PasswordHash = PASSWORD
                };

                var result = await userManager.CreateAsync(user, PASSWORD);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
                }
            }
        }
    }
}
