using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DogeNews.Data.Contracts
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All { get; }

        int Count { get; }

        T GetById(object id);

        T GetFirst(Expression<Func<T, bool>> filterExpression);

        TDestitanion GetFirstMapped<TDestitanion>(Expression<Func<T, bool>> filterExpression);

        IEnumerable<T> GetAll();

        IEnumerable<TDestination> GetAllMapped<TDestination>();

        IEnumerable<T> GetAll(Expression<Func<T, bool>> filterExpression);

        IEnumerable<TDestination> GetAllMapped<TDestination>(Expression<Func<T, bool>> filterExpression);
        
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}