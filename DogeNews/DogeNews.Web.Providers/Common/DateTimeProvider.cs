using System;

using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.Providers.Common
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}