using System.Web;

using DogeNews.Services.Http.Contracts;

namespace DogeNews.Services.Http
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