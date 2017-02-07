using System.Collections.Specialized;

namespace DogeNews.Web.Services.Contracts.Http
{
    public interface IHttpUtilityService
    {
        NameValueCollection ParseQueryString(string query);
    }
}
