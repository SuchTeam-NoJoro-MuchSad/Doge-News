using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DogeNews.Common.Validators;
using DogeNews.Data.Contracts;
using DogeNews.Services.Common.Contracts;

namespace DogeNews.Data.Repositories
{
    public class ProjectableRepository<T> : Repository<T>, IProjectableRepository<T> where T : class
    {
        private readonly IProjectionService projectionService;

        public ProjectableRepository(INewsDbContext context, IProjectionService projectionService)
            : base(context)
        {
            Validator.ValidateThatObjectIsNotNull(context,nameof(context));
            Validator.ValidateThatObjectIsNotNull(projectionService, nameof(projectionService));

            this.projectionService = projectionService;
        }

        public TDestitanion GetFirstMapped<TDestitanion>(Expression<Func<T, bool>> filterExpression)
        {
            IQueryable<T> query = this.All.Where(filterExpression);
            TDestitanion foundEntity = this.projectionService.ProjectToFirstOrDefault<T, TDestitanion>(query);

            return foundEntity;
        }

        public IEnumerable<TDestination> GetAllMapped<TDestination>()
        {
            List<TDestination> mappedEntities = this.projectionService.ProjectToList<T, TDestination>(this.All);

            return mappedEntities;
        }

        public IEnumerable<TDestination> GetAllMapped<TDestination>(Expression<Func<T, bool>> filterExpression)
        {
            IQueryable<T> query = this.All.Where(filterExpression);
            List<TDestination> mappedEntities = this.projectionService.ProjectToList<T, TDestination>(query);

            return mappedEntities;
        }
    }
}
