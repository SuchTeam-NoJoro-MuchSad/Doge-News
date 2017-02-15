using System.Collections.Generic;
using System.Linq;

namespace DogeNews.Services.Common
{
    public interface IProjectionService
    {
        TDestination ProjectToFirstOrDefault<TSource, TDestination>(IQueryable<TSource> query);

        List<TDestination> ProjectToList<TSource, TDestination>(IQueryable<TSource> query);
    }
}
