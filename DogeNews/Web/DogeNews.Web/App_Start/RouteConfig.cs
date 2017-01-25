using System.Web.Routing;

using Microsoft.AspNet.FriendlyUrls;

namespace DogeNews.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);
        }

        public static void RegisterCustomRoutes(RouteCollection routes)
        {
            RouteTable.Routes.MapPageRoute(
                routeName: "Settings", 
                routeUrl: "User/Settings/{username}", 
                physicalFile: "~/User/Settings/Settings.aspx");
        }
    }
}
