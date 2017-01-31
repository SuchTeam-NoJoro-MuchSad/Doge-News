using Ninject.Modules;

using DogeNews.Data.Contracts;
using DogeNews.Data.Repositories;
using DogeNews.Data;
using Ninject.Web.Common;

namespace DogeNews.Web.Bindings
{
    public class DataModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            this.Bind<INewsDbContext>().To<NewsDbContext>().InRequestScope();
            this.Bind<INewsData>().To<NewsData>();
        }
    }
}