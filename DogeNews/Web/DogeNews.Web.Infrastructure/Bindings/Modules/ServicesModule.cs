using Ninject.Modules;
using Ninject.Web.Common;

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
            this.Bind<INewsService>().To<NewsService>().InRequestScope();
            this.Bind<IFileService>().To<FileService>().InRequestScope();
            this.Bind<IArticleCommentsService>().To<ArticleCommentsService>().InRequestScope();
            this.Bind<IHttpContextService>().To<HttpContextService>().InRequestScope();
            this.Bind<IHttpPostedFileService>().To<HttpPostedFileService>().InRequestScope();
            this.Bind<IHttpServerUtilityService>().To<HttpServerUtilityService>().InRequestScope();
            this.Bind<IHttpUtilityService>().To<HttpUtilityService>().InRequestScope();
            this.Bind<IHttpResponseService>().To<HttpResponseService>().InRequestScope();
            this.Bind<IArticleManagementService>().To<ArticleManagementService>().InRequestScope();
            this.Bind<INotificationsService>().To<NotificationsService>().InRequestScope();
        }
    }
}