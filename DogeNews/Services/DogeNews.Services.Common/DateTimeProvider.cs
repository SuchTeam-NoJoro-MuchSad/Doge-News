using System;

using DogeNews.Services.Common.Contracts;

namespace DogeNews.Services.Common
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
    }
}
