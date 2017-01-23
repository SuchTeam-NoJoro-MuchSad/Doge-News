using System;
using System.Web;
using System.Collections.Generic;

using DogeNews.Web.Providers.Contracts;
using DogeNews.Web.Models;

namespace DogeNews.Web.Providers.Auth
{
    public class CookieProvider : ICookieProvider
    {
        private readonly IDateTimeProvider dateTimeProvider;

        public CookieProvider(IDateTimeProvider dateTimeProvider)
        {
            this.ValidateConstructorParams(dateTimeProvider);

            this.dateTimeProvider = dateTimeProvider;
        }

        public HttpCookie GetAuthenticationCookie(
            string cookieName,
            int daysUntilExpiration,
            IEnumerable<KeyValuePair<string, string>> values)
        {
            this.ValidateGetAuthenticationCookieParams(cookieName, daysUntilExpiration, values);
            //var creationDate = this.dateTimeProvider.Now;
            var expirationDate = this.dateTimeProvider.Now.AddDays(daysUntilExpiration);
            var cookie = new HttpCookie(cookieName);

            cookie.Expires = expirationDate;
            foreach (var pair in values)
            {
                cookie.Values.Add(pair.Key, pair.Value);
            }

            return cookie;
        }

        private void ValidateConstructorParams(IDateTimeProvider dateTimeProvider)
        {
            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException(nameof(dateTimeProvider));
            }
        }

        private void ValidateGetAuthenticationCookieParams(string cookieName,
            int daysUntilExpiration,
            IEnumerable<KeyValuePair<string, string>> values)
        {
            if (string.IsNullOrEmpty(cookieName))
            {
                throw new ArgumentNullException(nameof(cookieName));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }
        }
    }
}