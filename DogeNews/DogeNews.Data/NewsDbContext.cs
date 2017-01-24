using System.Data.Entity;

using DogeNews.Data.Contracts;
using DogeNews.Data.Migrations;
using DogeNews.Data.Models;

namespace DogeNews.Data
{
    public class NewsDbContext : DbContext, INewsDbContext
    {
        public NewsDbContext()
            : base("softsol_webforms") 
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NewsDbContext, Configuration>());
        }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Image> Images { get; set; }

        public IDbSet<NewsItem> NewsItems { get; set; }

        public IDbSet<Comment> Comments { get; set; }
        
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
