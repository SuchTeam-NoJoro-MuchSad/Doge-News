using System.Web;
using System.Collections.Specialized;

using DogeNews.Web.Services.Contracts.Http;

namespace DogeNews.Web.Services.Http
{
    public class HttpUtilityService : IHttpUtilityService
    {
        public NameValueCollection ParseQueryString(string query)
        {
            var result = HttpUtility.ParseQueryString(query);
            return result;
        }
    }
}
