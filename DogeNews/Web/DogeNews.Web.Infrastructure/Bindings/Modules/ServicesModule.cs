using Ninject.Modules;

using DogeNews.Web.Services.Contracts;
using DogeNews.Web.Services;

namespace DogeNews.Web.Infrastructure.Bindings.Modules
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<INewsService>().To<NewsService>();
            this.Bind<IFileService>().To<FileService>();
            this.Bind<IArticleCommentsService>().To<ArticleCommentsService>();         
        }
    }
}