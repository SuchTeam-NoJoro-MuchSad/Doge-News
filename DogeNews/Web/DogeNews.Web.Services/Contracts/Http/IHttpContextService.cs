using System.Web;

namespace DogeNews.Web.Services.Contracts.Http
{
    public interface IHttpContextService
    {
        string GetUsername(HttpContextBase httpContext);
    }
}
