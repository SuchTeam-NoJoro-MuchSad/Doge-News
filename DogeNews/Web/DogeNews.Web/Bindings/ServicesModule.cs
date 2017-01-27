using Ninject.Modules;

using DogeNews.Web.Services.Contracts;
using DogeNews.Web.Services;

namespace DogeNews.Web.Bindings
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<INewsService>().To<NewsService>();
            this.Bind<IFileService>().To<FileService>();
        }
    }
}