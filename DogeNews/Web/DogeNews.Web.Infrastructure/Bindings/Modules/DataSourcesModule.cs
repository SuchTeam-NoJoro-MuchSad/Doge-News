using Ninject.Modules;

using DogeNews.Web.DataSources.Contracts;
using DogeNews.Web.DataSources;

namespace DogeNews.Web.Infrastructure.Bindings.Modules
{
    public class DataSourcesModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(typeof(IDataSource<,>))
                .To(typeof(NewsDataSource))
                .Named("_Default");
        }
    }
}
