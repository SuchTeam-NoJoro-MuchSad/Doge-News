using DogeNews.Web.Providers;
using DogeNews.Web.Providers.Contracts;

using Ninject.Modules;
using Ninject.Web.Common;

namespace DogeNews.Web.Infrastructure.Bindings.Modules
{
    public class ProvidersModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IMapperProvider>().To<MapperProvider>().InSingletonScope();
            this.Bind<IDateTimeProvider>().To<DateTimeProvider>().InRequestScope();
        }
    }
}