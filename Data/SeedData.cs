using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using planty_compare_portal.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace planty_compare_portal.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = serviceProvider.GetRequiredService<MyIdentityDbContext>())
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@example.org");
                await EnsureRole(serviceProvider, adminID, Constants.AdminRole);

                // allowed user can create and edit contacts that they create
                var managerID = await EnsureUser(serviceProvider, testUserPw, "manager@example.org");
                await EnsureRole(serviceProvider, managerID, Constants.PurchasingPowerManagerRole);

                // SeedMyDB(context);
                using (var otherContext = serviceProvider.GetRequiredService<MyDbContext>())
                {
                    SeedMyDB(otherContext);
                }
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser { UserName = UserName };
                await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if(user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }
            
            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

        public static void SeedMyDB(MyDbContext context)
        {
	    /*
            if (context.PurchasingPower.Any())
            {
                return;   // DB has been seeded
            }

            // context.PurchasingPower.AddRange(
            //     new PurchasingPower
            //     {
            //         Year = 2000,
            //         City = "Berlin",
            //         Category = "G",
            //         Value = 66.95m
            //     },
            //     new PurchasingPower
            //     {
            //         Year = 2000,
            //         City = "Frankfurt",
            //         Category = "G",
            //         Value = 64.96m
            //     }
            // );
            // context.SaveChanges();
	    */
        }
    }
}
