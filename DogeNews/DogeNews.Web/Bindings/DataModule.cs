using Ninject.Modules;

using DogeNews.Data.Contracts;
using DogeNews.Data.Repositories;
using DogeNews.Data;

namespace DogeNews.Web.Bindings
{
    public class DataModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            this.Bind<INewsDbContext>().To<NewsDbContext>().InSingletonScope();
            this.Bind<INewsData>().To<NewsData>();
        }
    }
}