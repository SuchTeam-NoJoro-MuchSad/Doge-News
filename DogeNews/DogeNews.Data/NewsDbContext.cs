using System.Data.Entity;

using DogeNews.Data.Contracts;
using DogeNews.Data.Migrations;

namespace DogeNews.Data
{
    public class NewsDbContext : DbContext, INewsDbContext
    {
        public NewsDbContext()
            : base("NewsConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NewsDbContext, Configuration>());
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
