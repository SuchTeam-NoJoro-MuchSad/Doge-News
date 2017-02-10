using System.Web;

using DogeNews.Web.Services.Contracts.Http;

namespace DogeNews.Web.Services.Http
{
    public class HttpContextService : IHttpContextService
    {
        public string GetQueryStringPairValue(string key)
        {
            string value = HttpContext.Current.Request.QueryString[key];
            return value;
        }

        public string GetUsername(HttpContextBase httpContext)
        {
            string username = httpContext.User.Identity.Name;
            return username;
        }
    }
}