using System.Data.Entity;

using DogeNews.Data.Contracts;
using DogeNews.Data.Migrations;
using DogeNews.Data.Models;

using Microsoft.AspNet.Identity.EntityFramework;

namespace DogeNews.Data
{
    public class NewsDbContext : IdentityDbContext, INewsDbContext
    {
        public NewsDbContext()
            : base("DogeNews")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NewsDbContext, Configuration>());
        }

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

        public static NewsDbContext Create()
        {
            return new NewsDbContext();
        }
    }
}
