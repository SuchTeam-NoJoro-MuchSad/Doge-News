using System.Web;

using DogeNews.Web.Services.Contracts.Http;

namespace DogeNews.Web.Services.Http
{
    public class HttpContextService : IHttpContextService
    {
        public string GetUsername(HttpContextBase httpContext)
        {
            string username = httpContext.User.Identity.Name;
            return username;
        }
    }
}