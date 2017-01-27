using System.Data.Entity.Migrations;
using System.Linq;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using DogeNews.Data.Models;
using DogeNews.Common.Constants;

namespace DogeNews.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<NewsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(NewsDbContext context)
        {
            this.SeedRoles(context);
            this.SeedUsers(context);
        }

        private void SeedRoles(NewsDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!context.Roles.Any(r => r.Name == Roles.Admin))
            {
                var role = new IdentityRole { Name = Roles.Admin };
                roleManager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == Roles.Normal))
            {
                var role = new IdentityRole { Name = Roles.Normal };
                roleManager.Create(role);
            }
        }

        private void SeedUsers(NewsDbContext context)
        {
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            if (!context.Users.Any(u => u.UserName == "admin@admin.com"))
            {
                var adminUser = new User { UserName = "admin@admin.com" };
                
                var isSuccessful = userManager.Create(adminUser, "admin1");
                userManager.AddToRole(adminUser.Id, Roles.Admin);
            }
        }
    }
}
