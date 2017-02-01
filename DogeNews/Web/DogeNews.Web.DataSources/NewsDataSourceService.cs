using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.DataSources.Contracts;
using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.DataSources
{
    public class NewsDataSource : IDataSource<NewsItem, NewsWebModel>
    {
        private readonly IRepository<NewsItem> newsItemRepository;
        private readonly IMapperProvider mapperProvider;

        public NewsDataSource(IRepository<NewsItem> newsItemRepository, IMapperProvider mapperProvider)
        {
            this.ValidateConstructorParams(newsItemRepository, mapperProvider);

            this.newsItemRepository = newsItemRepository;
            this.mapperProvider = mapperProvider;
        }

        public int Count
        {
            get
            {
                return this.newsItemRepository.Count;
            }
        }

        public IEnumerable<NewsWebModel> GetPageItems(int page, int pageSize)
        {
            this.ValidatePage(page);
            this.ValidatePageSize(pageSize);

            var items = this.OrderByDescending(x => x.CreatedOn, page, pageSize);
            return items;
        }

        public IEnumerable<NewsWebModel> OrderByAscending<TKey>(Expression<Func<NewsItem, TKey>> orderExpression, int page, int pageSize)
        {
            this.ValidatePage(page);
            this.ValidatePageSize(pageSize);

            var items = this.newsItemRepository
                .All
                .OrderBy(orderExpression)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(x => this.mapperProvider.Instance.Map<NewsWebModel>(x));

            return items;
        }

        public IEnumerable<NewsWebModel> OrderByDescending<TKey>(Expression<Func<NewsItem, TKey>> orderExpression, int page, int pageSize)
        {
            this.ValidatePage(page);
            this.ValidatePageSize(pageSize);

            var items = this.newsItemRepository
               .All
               .OrderByDescending(orderExpression)
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToList()
               .Select(x => this.mapperProvider.Instance.Map<NewsWebModel>(x));

            return items;
        }

        private void ValidateConstructorParams(IRepository<NewsItem> newsItemRepository, IMapperProvider mapperProvider)
        {
            if (newsItemRepository == null)
            {
                throw new ArgumentNullException("newsItemRepository");
            }

            if (mapperProvider == null)
            {
                throw new ArgumentNullException("mapperProvider");
            }
        }

        private void ValidatePage(int page)
        {
            if (page <= 0)
            {
                throw new ArgumentOutOfRangeException("page");
            }
        }

        private void ValidatePageSize(int pageSize)
        {
            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("pageSize");
            }
        }
    }
}
