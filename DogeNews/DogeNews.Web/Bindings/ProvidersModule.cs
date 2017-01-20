using Ninject.Modules;

using DogeNews.Web.Providers.Contracts;
using DogeNews.Web.Providers;

namespace DogeNews.Web.Bindings
{
    public class ProvidersModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IMapperProvider>().To<MapperProvider>().InSingletonScope();
            this.Bind<ICryptoServiceSaltProvider>().To<RngCryptoServiceSaltProvider>();
            this.Bind<ICryptoServiceHashProvider>().To<RfcCryptoServiceHashProvider>();
        }
    }
}