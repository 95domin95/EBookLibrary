using EBookLibraryData.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibraryData.Models.Seeders
{
    public class AdminAccountSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var (userManager, adminAccountOptions) = GetRequiredServices(scope.ServiceProvider);

                if (await AdminAlreadyExists(userManager, adminAccountOptions))
                {
                    return;
                }

                await CreateAdminAccount(userManager, adminAccountOptions);
            }
        }

        private static (UserManager<ApplicationUser>, AdminAccountOptions)
            GetRequiredServices(IServiceProvider serviceProvider)
        {
            return (
                    serviceProvider.GetRequiredService<UserManager<ApplicationUser>>(),
                    serviceProvider.GetRequiredService<IOptions<AdminAccountOptions>>().Value
                );
        }

        private static async Task<bool> AdminAlreadyExists(UserManager<ApplicationUser> userManager,
            AdminAccountOptions adminAccountOptions)
        {
            var existingAdminUser = await userManager.FindByNameAsync(adminAccountOptions.UserName);
            return existingAdminUser is null ? false : true;
        }

        private static async Task CreateAdminAccount(UserManager<ApplicationUser> userManager,
        AdminAccountOptions adminAccountOptions)
        {
            var adminUser = new ApplicationUser
            {
                UserName = adminAccountOptions.UserName,
                Email = adminAccountOptions.Email
            };
            await userManager.CreateAsync(adminUser, adminAccountOptions.Password);
            await userManager.AddToRoleAsync(adminUser, Roles.Admin);
        }
    }
}
