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
        private readonly IRepository<NewsItem> repository;
        private readonly IMapperProvider mapperProvider;

        public NewsDataSourceService(IRepository<NewsItem> repository, IMapperProvider mapperProvider)
        {
            this.repository = repository;
            this.mapperProvider = mapperProvider;
        }

        public int Count
        {
            get
            {
                return this.repository.Count;
            }
        }

        public IEnumerable<NewsWebModel> GetPageItems(int page, int pageSize)
        {
            var items = this.repository
                .All
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(x => this.mapperProvider.Instance.Map<NewsWebModel>(x));

            return items;
        }
    }
}
