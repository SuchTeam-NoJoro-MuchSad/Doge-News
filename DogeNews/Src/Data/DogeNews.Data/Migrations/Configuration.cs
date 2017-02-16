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
            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!context.Roles.Any(r => r.Name == Roles.Admin))
            {
                IdentityRole role = new IdentityRole { Name = Roles.Admin };
                roleManager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == Roles.Normal))
            {
                IdentityRole role = new IdentityRole { Name = Roles.Normal };
                roleManager.Create(role);
            }
        }

        private void SeedUsers(NewsDbContext context)
        {
            UserStore<User> userStore = new UserStore<User>(context);
            UserManager<User> userManager = new UserManager<User>(userStore);

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                User adminUser = new User { UserName = "admin" };
                
                IdentityResult isSuccessful = userManager.Create(adminUser, "admin1");
                userManager.AddToRole(adminUser.Id, Roles.Admin);
            }
        }
    }
}
