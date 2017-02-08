using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DogeNews.Common.Enums;
using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.DataSources.Contracts;
using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.DataSources
{
    public class NewsDataSource : INewsDataSource<NewsItem, NewsWebModel>
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
            return this.GetPageItems(page, pageSize, null);
        }

        public IEnumerable<NewsWebModel> GetPageItems(int page, int pageSize, string category)
        {
            this.ValidatePage(page);
            this.ValidatePageSize(pageSize);

            if (string.IsNullOrEmpty(category))
            {
                return this.OrderByDescending(x => x.CreatedOn, page, pageSize);
            }


            var items = this.OrderByDescending(x => x.CreatedOn, page, pageSize, category);
            return items;
        }

        public IEnumerable<NewsWebModel> OrderByAscending<TKey>(Expression<Func<NewsItem, TKey>> orderExpression, int page, int pageSize)
        {
            return this.OrderByAscending(orderExpression, page, pageSize, null);
        }

        public IEnumerable<NewsWebModel> OrderByAscending<TKey>(Expression<Func<NewsItem, TKey>> orderExpression, int page, int pageSize, string category)
        {
            this.ValidatePage(page);
            this.ValidatePageSize(pageSize);

            var result = this.newsItemRepository.All;

            if (!string.IsNullOrEmpty(category))
            {
                var newsCategoryType = (NewsCategoryType)Enum.Parse(typeof(NewsCategoryType), category);

                result = result.Where(x => x.Category == newsCategoryType);
            }

            var items = result
                .OrderBy(orderExpression)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(x => this.mapperProvider.Instance.Map<NewsWebModel>(x));

            return items;
        }

        public IEnumerable<NewsWebModel> OrderByDescending<TKey>(Expression<Func<NewsItem, TKey>> orderExpression, int page, int pageSize)
        {
            return this.OrderByDescending(orderExpression, page, pageSize, null);
        }

        public IEnumerable<NewsWebModel> OrderByDescending<TKey>(Expression<Func<NewsItem, TKey>> orderExpression, int page, int pageSize, string category)
        {
            this.ValidatePage(page);
            this.ValidatePageSize(pageSize);

            var result = this.newsItemRepository.All;

            if (!string.IsNullOrEmpty(category))
            {
                var newsCategoryType = (NewsCategoryType)Enum.Parse(typeof(NewsCategoryType), category);

                result = result.Where(x => x.Category == newsCategoryType);
            }

            var items = result
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
