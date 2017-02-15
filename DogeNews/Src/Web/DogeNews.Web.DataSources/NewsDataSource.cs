using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using DogeNews.Common.Enums;
using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.DataSources.Contracts;
using DogeNews.Common.Validators;
using DogeNews.Services.Common.Contracts;

namespace DogeNews.Web.DataSources
{
    public class NewsDataSource : INewsDataSource<NewsItem, NewsWebModel>
    {
        private readonly IRepository<NewsItem> newsItemRepository;
        private readonly IMapperProvider mapperProvider;

        private int count;

        public NewsDataSource(IRepository<NewsItem> newsItemRepository, IMapperProvider mapperProvider)
        {
            Validator.ValidateThatObjectIsNotNull(newsItemRepository, nameof(newsItemRepository));
            Validator.ValidateThatObjectIsNotNull(mapperProvider, nameof(mapperProvider));

            this.newsItemRepository = newsItemRepository;
            this.Count = this.newsItemRepository.Count;

            this.mapperProvider = mapperProvider;
        }

        public int Count
        {
            get { return this.count; }

            set { this.count = value; }
        }

        public IEnumerable<NewsWebModel> GetPageItems(int page, int pageSize, bool isAdminUser, string category = null)
        {
            Validator.ValidateThatNumberIsNotNegative(page, nameof(page));
            Validator.ValidateThatNumberIsNotNegative(pageSize, nameof(pageSize));

            var items = this.OrderByDescending(x => x.CreatedOn, page, pageSize, isAdminUser, category);
            return items;
        }

        public IEnumerable<NewsWebModel> OrderByAscending<TKey>(Expression<Func<NewsItem, TKey>> orderExpression, int page, int pageSize, bool isAdminUser, string category = null)
        {
            Validator.ValidateThatNumberIsNotNegative(page, nameof(page));
            Validator.ValidateThatNumberIsNotNegative(pageSize, nameof(pageSize));

            var result = this.GetNews(category, isAdminUser);
            var items = result
                .OrderBy(orderExpression)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(x => this.mapperProvider.Instance.Map<NewsWebModel>(x));
            
            return items;
        }

        public IEnumerable<NewsWebModel> OrderByDescending<TKey>(Expression<Func<NewsItem, TKey>> orderExpression, int page, int pageSize, bool isAdminUser, string category = null)
        {
            Validator.ValidateThatNumberIsNotNegative(page, nameof(page));
            Validator.ValidateThatNumberIsNotNegative(pageSize, nameof(pageSize));

            var news = this.GetNews(category, isAdminUser);
            var items = news
               .OrderByDescending(orderExpression)
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToList()
               .Select(x => this.mapperProvider.Instance.Map<NewsWebModel>(x));

            return items;
        }

        private IQueryable<NewsItem> GetNews(string category, bool isAdminUser)
        {
            var news = this.newsItemRepository.All;

            if (!string.IsNullOrEmpty(category))
            {
                var newsCategoryType = (NewsCategoryType)Enum.Parse(typeof(NewsCategoryType), category);
                news = news.Where(x => x.Category == newsCategoryType);
            }

            if (!isAdminUser)
            {
                news = news.Where(x => x.DeletedOn == null);
            }

            this.Count = news.Count();
            return news;
        }
    }
}
