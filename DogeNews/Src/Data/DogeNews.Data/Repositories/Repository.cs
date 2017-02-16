using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using DogeNews.Data.Contracts;
using DogeNews.Common.Validators;

namespace DogeNews.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly INewsDbContext context;
        private readonly IDbSet<T> dbSet;

        public Repository(INewsDbContext context)
        {
            this.context = context;

            this.dbSet = this.context.Set<T>();
        }

        public INewsDbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IDbSet<T> DbSet
        {
            get
            {
                return this.dbSet;
            }
        }

        public IQueryable<T> All
        {
            get
            {
                return this.DbSet;
            }
        }

        public int Count
        {
            get
            {
                return this.DbSet.Count();
            }
        }

        public T GetFirst(Expression<Func<T, bool>> filterExpression)
        {
            Validator.ValidateThatObjectIsNotNull(filterExpression, nameof(filterExpression));

            T foundEntity = this.DbSet.FirstOrDefault(filterExpression);
            return foundEntity;
        }
                
        public IEnumerable<T> GetAll()
        {
            return this.GetAll(null);
        }
        
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filterExpression)
        {
            return this.context.Set<T>().ToList();
        }
        
        public T GetById(object id)
        {
            Validator.ValidateThatObjectIsNotNull(id, nameof(id));
            
            return this.DbSet.Find(id);
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            DbEntityEntry entry = AttachIfDetached(entity);
            entry.State = EntityState.Added;
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            DbEntityEntry entry = AttachIfDetached(entity);
            entry.State = EntityState.Deleted;
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            DbEntityEntry entry = AttachIfDetached(entity);
            entry.State = EntityState.Modified;
        }

        private DbEntityEntry AttachIfDetached(T entity)
        {
            DbEntityEntry<T> entry = this.Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            return entry;
        }
    }
}
