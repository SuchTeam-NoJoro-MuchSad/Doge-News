using System;

namespace DogeNews.Web.Providers.Contracts
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
