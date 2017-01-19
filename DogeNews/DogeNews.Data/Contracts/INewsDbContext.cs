using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DogeNews.Data.Contracts
{
    public interface INewsDbContext : IDisposable
    {
        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        void SaveChanges();
    }
}
