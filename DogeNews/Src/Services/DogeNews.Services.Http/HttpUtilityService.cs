using System.Web;
using System.Collections.Specialized;

using DogeNews.Services.Http.Contracts;

namespace DogeNews.Services.Http
{
    public class HttpUtilityService : IHttpUtilityService
    {
        public NameValueCollection ParseQueryString(string query)
        {
            NameValueCollection result = HttpUtility.ParseQueryString(query);
            return result;
        }
    }
}
