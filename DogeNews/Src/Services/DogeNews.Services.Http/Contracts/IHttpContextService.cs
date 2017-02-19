using System.Web;

namespace DogeNews.Services.Http.Contracts
{
    public interface IHttpContextService
    {
        string GetLoggedInUserUsername();

        string GetQueryStringPairValue(string key);
    }
}
