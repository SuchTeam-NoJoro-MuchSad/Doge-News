using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DogeNews.Web.DataSources.Contracts
{
    public interface INewsDataSource<TNewsItem, TNewsWebModel> : IDataSource<TNewsItem, TNewsWebModel> where TNewsItem : class
    {
        IEnumerable<TNewsWebModel> GetPageItems(int page, int pageSize, bool isAdminUser, string category);

        IEnumerable<TNewsWebModel> OrderByAscending<TKey>(Expression<Func<TNewsItem, TKey>> orderExpression, int page, int pageSize, bool isAdminUser, string category);

        IEnumerable<TNewsWebModel> OrderByDescending<TKey>(Expression<Func<TNewsItem, TKey>> orderExpression, int page, int pageSize, bool isAdminUser, string category);
    }
}
