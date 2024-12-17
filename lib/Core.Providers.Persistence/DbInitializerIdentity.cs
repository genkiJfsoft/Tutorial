using ExpenseTracker.Core.Domain.Authorizations;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Core.Providers.Persistence;


public static partial class DbInitializer
{
    /// <summary>
    /// Defines a custom initialization logic for Identity roles and users in the database.
    /// </summary>
    public static class Identity
    {
        public const string DefaultUserId = "487ae23c-376a-4792-8612-aad838e779cc"; // ? Maybe move to config
        private const string DefaultUserPassword = "Qwe123!";

        /// <summary>
        /// Ensures that all roles defined in the application are created in the database.
        /// </summary>
        /// <remarks>
        /// This method retrieves a list of role names from the <see cref="Roles"/> constants, checks if each role exists,
        /// and creates it if it does not.
        /// </remarks>
        public static async Task EnsureRolesAsync(IServiceProvider serviceProvider)
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("DbInitializer.Identity");

            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
                var roleNames = Roles.GetValues();

                foreach (var roleName in roleNames)
                {
                    if (await roleManager.RoleExistsAsync(roleName)) continue;
                    logger.LogInformation("Creating {RoleName} role...", roleName);
                    var role = new Role(roleName);
                    await roleManager.CreateAsync(role);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating roles.");
                throw;
            }
        }

        /// <summary>
        /// Ensures that a default user is created in the database.
        /// </summary>
        /// <remarks>
        /// This method checks if there are any users in the database. If there are none, it creates a default user with the
        /// specified information and adds it to the <see cref="Roles.User.Admin"/> role.
        /// </remarks>
        public static async Task EnsureDefaultUserAsync(IServiceProvider serviceProvider)
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("DbInitializer.Identity");

            try
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

                if (await userManager.Users.AnyAsync()) return;

                logger.LogInformation("Creating default user...");

                var user = new User("admin")
                {
                    Id = DefaultUserId,
                    DisplayName = "Default Admin",
                    Email = "admin@example.com",
                    EmailConfirmed = true,
                };

                await userManager.CreateAsync(user, DefaultUserPassword);
                await userManager.AddToRoleAsync(user, Roles.User.Admin);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating the default user.");
                throw;
            }
        }
    }
}
