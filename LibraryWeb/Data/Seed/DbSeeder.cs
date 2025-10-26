using LibraryWeb.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryWeb.Data.Seed
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("Member"))
                await roleManager.CreateAsync(new IdentityRole("Member"));
        }

        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {
            var adminEmail = "admin@library.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "System Admin",
                    EmailConfirmed = true,
                };

                var result = await userManager.CreateAsync(user, "Admin@123");

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
