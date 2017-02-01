using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DogeNews.Web.DataSources.Contracts
{
    public interface IDataSource<DbModelType, TargetMapType> where DbModelType : class
    {
        int Count { get; }

        IEnumerable<TargetMapType> GetPageItems(int page, int pageSize);

        IEnumerable<TargetMapType> OrderByAscending<TKey>(Expression<Func<DbModelType, TKey>> orderExpression, int page, int pageSize);

        IEnumerable<TargetMapType> OrderByDescending<TKey>(Expression<Func<DbModelType, TKey>> orderExpression, int page, int pageSize);
    }
}
