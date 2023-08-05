using Microsoft.AspNetCore.Identity;

namespace TaskifyAuthService.Web.Utils
{
    public static class AdminUserInitializer
    {
        public static void SeedAdmin(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@admin").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "ADMIN",
                    Email = "admin@admin.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "Admin@123").Result;

                //if (result.Succeeded)
                //{
                //    userManager.AddToRoleAsync(user, "Admin").Wait();
                //}
            }
        }
    }
}
