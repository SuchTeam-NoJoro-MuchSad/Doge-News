using System.Data.Entity.Migrations;

namespace DogeNews.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<NewsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NewsDbContext context)
        {
        }
    }
}
