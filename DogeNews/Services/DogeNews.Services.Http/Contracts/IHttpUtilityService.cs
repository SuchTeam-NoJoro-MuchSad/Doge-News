using System.Collections.Specialized;

namespace DogeNews.Services.Http.Contracts
{
    public interface IHttpUtilityService
    {
        NameValueCollection ParseQueryString(string query);
    }
}
