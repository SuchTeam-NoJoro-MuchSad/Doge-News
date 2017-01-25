using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

using DogeNews.Web.App_Start;
using DogeNews.Web.Providers.Contracts;
using DogeNews.Web.Services.Contracts;

using Ninject;

namespace DogeNews.Web
{
    public class Global : HttpApplication
    {
        public void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            RouteConfig.RegisterCustomRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BindingsConfig.BindPresenterFactory();

            var mapperProvider = NinjectWebCommon.Kernel.Get<IMapperProvider>();
            (new MappingsConfig(mapperProvider)).Map();

            var authSevice = NinjectWebCommon.Kernel.Get<IAuthService>();
            authSevice.SeedAdminUser();
        }
    }
}