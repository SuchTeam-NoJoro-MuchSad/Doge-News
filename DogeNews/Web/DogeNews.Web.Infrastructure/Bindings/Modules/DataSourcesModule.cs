using Ninject.Modules;
using Ninject.Web.Common;

using DogeNews.Web.DataSources.Contracts;
using DogeNews.Web.DataSources;
using DogeNews.Web.Models;
using DogeNews.Data.Models;

namespace DogeNews.Web.Infrastructure.Bindings.Modules
{
    public class DataSourcesModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<INewsDataSource<NewsItem, NewsWebModel>>()
                .To<NewsDataSource>()
                .InRequestScope();
        }
    }
}
