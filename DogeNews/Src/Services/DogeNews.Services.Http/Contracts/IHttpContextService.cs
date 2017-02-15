using System.Web;

namespace DogeNews.Services.Http.Contracts
{
    public interface IHttpContextService
    {
        string GetUsername(HttpContextBase httpContext);

        string GetQueryStringPairValue(string key);
    }
}
