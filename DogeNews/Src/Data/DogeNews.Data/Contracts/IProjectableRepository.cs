using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DogeNews.Data.Contracts
{
    public interface IProjectableRepository<T> : IRepository<T> where T : class
    {
        TDestitanion GetFirstMapped<TDestitanion>(Expression<Func<T, bool>> filterExpression);

        IEnumerable<TDestination> GetAllMapped<TDestination>();

        IEnumerable<TDestination> GetAllMapped<TDestination>(Expression<Func<T, bool>> filterExpression);
    }
}
