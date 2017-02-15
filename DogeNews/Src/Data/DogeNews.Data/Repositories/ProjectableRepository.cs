using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using DogeNews.Data.Contracts;
using DogeNews.Services.Common;

namespace DogeNews.Data.Repositories
{
    public class ProjectableRepository<T> : Repository<T>, IProjectableRepository<T> where T : class
    {
        private readonly IProjectionService projectionService;

        public ProjectableRepository(INewsDbContext context, IProjectionService projectionService)
            : base(context)
        {
            this.projectionService = projectionService;
        }

        public TDestitanion GetFirstMapped<TDestitanion>(Expression<Func<T, bool>> filterExpression)
        {
            var query = this.All.Where(filterExpression);
            var foundEntity = this.projectionService.ProjectToFirstOrDefault<T, TDestitanion>(query);

            return foundEntity;
        }

        public IEnumerable<TDestination> GetAllMapped<TDestination>()
        {
            var mappedEntities = this.projectionService.ProjectToList<T, TDestination>(this.All);

            return mappedEntities;
        }

        public IEnumerable<TDestination> GetAllMapped<TDestination>(Expression<Func<T, bool>> filterExpression)
        {
            var query = this.All.Where(filterExpression);
            var mappedEntities = this.projectionService.ProjectToList<T, TDestination>(query);

            return mappedEntities;
        }
    }
}
