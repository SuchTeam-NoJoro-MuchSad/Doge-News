using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using DogeNews.Data.Models;

namespace DogeNews.Data.Contracts
{
    public interface INewsDbContext : IDisposable
    {
        IDbSet<T> Set<T>() where T : class;
        
        IDbSet<Image> Images { get; set; }

        IDbSet<NewsItem> NewsItems { get; set; }

        IDbSet<Comment> Comments { get; set; }

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        int SaveChanges();
    }
}
