using DogeNews.Web.MVP.News.Article;
using Ninject.Modules;

using DogeNews.Web.Services.Contracts;
using DogeNews.Web.Services;
using DogeNews.Web.MVP.UserControls.NewsGrid;

namespace DogeNews.Web.Bindings
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<INewsService>().To<NewsService>();
            this.Bind<IFileService>().To<FileService>();

            this.Bind(typeof(IDataSourceService<,>))
                .To(typeof(NewsDataSourceService))
                .WhenInjectedInto<NewsGridPresenter>();

            this.Bind(typeof(IDataSourceService<,>))
                .To(typeof(NewsDataSourceService))
                .WhenInjectedInto<ArticlePresenter>();
        }
    }
}