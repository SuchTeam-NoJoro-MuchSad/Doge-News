using System.Collections.Generic;
using System.Linq;

using DogeNews.Common.Validators;
using DogeNews.Services.Common.Contracts;

using AutoMapper;

namespace DogeNews.Services.Common
{
    public class ProjectionService : IProjectionService
    {
        private readonly IMapperProvider mapperProvider;

        public ProjectionService(IMapperProvider mapperProvider)
        {
            Validator.ValidateThatObjectIsNotNull(mapperProvider, nameof(mapperProvider));

            this.mapperProvider = mapperProvider;
        }

        public TDestination ProjectToFirstOrDefault<TSource, TDestination>(IQueryable<TSource> query)
        {
            TDestination projectedItem = query.ProjectToFirstOrDefault<TDestination>(this.mapperProvider.Configuration);
            return projectedItem;
        }

        public List<TDestination> ProjectToList<TSource, TDestination>(IQueryable<TSource> query)
        {
            List<TDestination> projectedCollection = query.ProjectToList<TDestination>(this.mapperProvider.Configuration);
            return projectedCollection;
        }
    }
}
