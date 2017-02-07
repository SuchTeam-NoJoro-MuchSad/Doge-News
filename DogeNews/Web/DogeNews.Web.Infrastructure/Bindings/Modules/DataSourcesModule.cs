using DogeNews.Data.Models;
using Ninject.Modules;

using DogeNews.Web.DataSources.Contracts;
using DogeNews.Web.DataSources;
using DogeNews.Web.Models;
using DogeNews.Web.Mvp.UserControls.NewsGrid;

namespace DogeNews.Web.Infrastructure.Bindings.Modules
{
    public class DataSourcesModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<INewsDataSource<NewsItem, NewsWebModel>>()
                .To<NewsDataSource>()
                .WhenInjectedExactlyInto<NewsGridPresenter>();
        }
    }
}
