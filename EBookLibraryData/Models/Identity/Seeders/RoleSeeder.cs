using EBookLibraryData.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibraryData.Models.Seeders
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServiceProvider = scope.ServiceProvider;
                var roleManager = scopedServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                await roleManager.AddRoleAsync(Roles.Admin);
                await roleManager.AddRoleAsync(Roles.Moderator);
                await roleManager.AddRoleAsync(Roles.User);
            }
        }

        private static async Task AddRoleAsync(this RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole(roleName);
                await roleManager.CreateAsync(role);
            }
        }
    }
}
