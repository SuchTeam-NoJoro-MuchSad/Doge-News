using Ninject.Modules;
using Ninject.Web.Common;

using DogeNews.Data.Contracts;
using DogeNews.Data.Repositories;
using DogeNews.Data;

namespace DogeNews.Web.Infrastructure.Bindings.Modules
{
    public class DataModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();
            this.Bind<INewsDbContext>().To<NewsDbContext>().InRequestScope();
            this.Bind<INewsData>().To<NewsData>().InRequestScope();
        }
    }
}