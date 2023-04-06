using JournalsAndAuth.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JournalsAndAuth.Models
{
    public static class SeedMethod
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            JournalsContext context = new JournalsContext(serviceProvider.GetRequiredService<DbContextOptions<JournalsContext>>());
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!context.Roles.Any())
            {
                List<string> newRoles = new List<string> { "Administrator", "Moderator" };

                foreach(string r in newRoles)
                {
                    await roleManager.CreateAsync(new IdentityRole(r));
                }

                context.SaveChanges();
            }
        }
    }
}
