using System.Data.Entity.Migrations;

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
        }
    }
}
