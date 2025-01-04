using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Mudemy.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mudemy.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<UserApp>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "User", "Instructor" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var users = new List<(string Email, string UserName, string Role)>
            {
                ("user1@example.com", "user1", "User"),
                ("user2@example.com", "user2", "User"),
                ("instructor@example.com", "instructor", "Instructor")
            };

            foreach (var (email, username, role) in users)
            {
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new UserApp
                    {
                        UserName = username,
                        Email = email
                    };

                    var result = await userManager.CreateAsync(user, "Password123!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
            }
        }
    }
}