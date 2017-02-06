using Ninject.Modules;

using DogeNews.Web.Services.Contracts;
using DogeNews.Web.Services;
using DogeNews.Web.Services.Contracts.Http;
using DogeNews.Web.Services.Http;

namespace DogeNews.Web.Infrastructure.Bindings.Modules
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<INewsService>().To<NewsService>();
            this.Bind<IFileService>().To<FileService>();
            this.Bind<IArticleCommentsService>().To<ArticleCommentsService>();
            this.Bind<IHttpContextService>().To<HttpContextService>();
            this.Bind<IHttpPostedFileService>().To<HttpPostedFileService>();
            this.Bind<IHttpServerUtilityService>().To<HttpServerUtilityService>(); 
        }
    }
}