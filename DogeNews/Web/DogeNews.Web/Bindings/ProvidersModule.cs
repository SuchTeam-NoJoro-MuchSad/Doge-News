using Ninject.Modules;

using DogeNews.Web.Providers.Contracts;
using DogeNews.Web.Providers.Config;
using DogeNews.Web.Providers.Auth;
using DogeNews.Web.Providers.Encryption;
using DogeNews.Web.Providers.Common;

namespace DogeNews.Web.Bindings
{
    public class ProvidersModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IMapperProvider>().To<MapperProvider>().InSingletonScope();
            this.Bind<ICryptoServiceSaltProvider>().To<RngCryptoServiceSaltProvider>();
            this.Bind<ICryptoServiceHashProvider>().To<RfcCryptoServiceHashProvider>();
            this.Bind<IEncryptionProvider>().To<EncryptionProvider>().InSingletonScope();
            this.Bind<ICookieProvider>().To<CookieProvider>();
            this.Bind<IAppConfigurationProvider>().To<AppConfigurationProvider>();
            this.Bind<IDateTimeProvider>().To<DateTimeProvider>();
            this.Bind<IFileProvider>().To<FileProvider>();
        }
    }
}