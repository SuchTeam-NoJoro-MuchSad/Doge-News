using System;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;

using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.Providers
{
    public class CookieProvider : ICookieProvider
    {
        public HttpCookie GetAuthenticationCookie(
            string cookieName,
            int daysUntilExpiration,
            IEnumerable<KeyValuePair<string, string>> values)
        {
            var creationDate = DateTime.Now;
            var expirationDate = DateTime.Now.AddDays(daysUntilExpiration);
            var cookiePath = FormsAuthentication.FormsCookiePath;
            var cookie = new HttpCookie(cookieName);
            
            cookie.Expires = expirationDate;
            foreach (var pair in values)
            {
                cookie.Values.Add(pair.Key, pair.Value);
            }

            return cookie;
        }
    }
}