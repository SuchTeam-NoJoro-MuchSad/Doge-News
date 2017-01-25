using DogeNews.Web.Services.Contracts;
using DogeNews.Web.Services;

using Ninject.Modules;

namespace DogeNews.Web.Bindings
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IAuthService>().To<AuthService>();
            this.Bind<ICryptographicService>().To<CryptographicService>();
            this.Bind<INewsService>().To<NewsService>();
            this.Bind<IUserService>().To<UserService>();
        }
    }
}