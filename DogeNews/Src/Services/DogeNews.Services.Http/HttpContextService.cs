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

        public string GetLoggedInUserUsername()
        {
            string username = HttpContext.Current.User.Identity.Name;
            return username;
        }
    }
}