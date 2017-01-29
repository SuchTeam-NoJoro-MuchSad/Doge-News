using System;
using System.Collections.Generic;
using System.Linq;

using DogeNews.Data.Contracts;
using DogeNews.Web.Providers.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.Services.Contracts;

namespace DogeNews.Web.Services
{
    public class NewsDataSourceService : IDataSourceService<NewsItem, NewsWebModel>
    {
        private readonly IRepository<NewsItem> newsItemRepository;
        private readonly IMapperProvider mapperProvider;

        public NewsDataSourceService(IRepository<NewsItem> newsItemRepository, IMapperProvider mapperProvider)
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

            var items = this.OrderByDateDescending(page, pageSize);
            return items;
        }

        public IEnumerable<NewsWebModel> OrderByDateAscending(int page, int pageSize)
        {
            this.ValidatePage(page);
            this.ValidatePageSize(pageSize);

            var items = this.newsItemRepository
                .All
                .OrderBy(x => x.CreatedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(x => this.mapperProvider.Instance.Map<NewsWebModel>(x));

            return items;
        }

        public IEnumerable<NewsWebModel> OrderByDateDescending(int page, int pageSize)
        {
            this.ValidatePage(page);
            this.ValidatePageSize(pageSize);

            var items = this.newsItemRepository
               .All
               .OrderByDescending(x => x.CreatedOn)
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToList()
               .Select(x => this.mapperProvider.Instance.Map<NewsWebModel>(x));

            return items;
        }

        public NewsWebModel GetNewsItemByTitle(string title)
        {
            var foundNewsItem = this.newsItemRepository.GetFirst(x => x.Title == title);
            return this.mapperProvider.Instance.Map<NewsWebModel>(foundNewsItem);
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
