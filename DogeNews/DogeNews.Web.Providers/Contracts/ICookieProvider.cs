using System.Collections.Generic;
using System.Web;

namespace DogeNews.Web.Providers.Contracts
{
    public interface ICookieProvider
    {
        HttpCookie GetAuthenticationCookie(
            string cookieName,
            int daysUntilExpiration,
            IEnumerable<KeyValuePair<string, string>> values);
    }
}
