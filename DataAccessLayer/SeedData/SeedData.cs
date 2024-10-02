//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.DependencyInjection;

//public static class SeedData
//{
//    public static async Task Initialize(IServiceProvider serviceProvider)
//    {
//        using (var scope = serviceProvider.CreateScope())
//        {
//            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//            //var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

//            string[] roleNames = { "HR", "Employee" };
//            IdentityResult roleResult;

//            foreach (var roleName in roleNames)
//            {
//                var roleExist = await roleManager.RoleExistsAsync(roleName);
//                if (!roleExist)
//                {
//                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
//                }
//            }

//            //var user = await userManager.FindByEmailAsync("hr@example.com");

//            if (user == null)
//            {
//                //user = new ApplicationUser()
//                //{
//                //    UserName = "hr@example.com",
//                //    Email = "hr@example.com"
//                //};
//                await userManager.CreateAsync(user, "Test@123");
//            }
//            await userManager.AddToRoleAsync(user, "HR");
//        }
//    }
//}
