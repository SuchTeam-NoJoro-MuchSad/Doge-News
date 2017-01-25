using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DogeNews.Web.Startup))]
namespace DogeNews.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
