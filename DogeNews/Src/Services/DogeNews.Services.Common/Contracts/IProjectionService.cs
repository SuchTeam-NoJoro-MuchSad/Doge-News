using System.Collections.Generic;
using System.Linq;

namespace DogeNews.Services.Common.Contracts
{
    public interface IProjectionService
    {
        TDestination ProjectToFirstOrDefault<TSource, TDestination>(IQueryable<TSource> query);

        List<TDestination> ProjectToList<TSource, TDestination>(IQueryable<TSource> query);
    }
}
