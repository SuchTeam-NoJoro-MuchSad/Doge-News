using System;

namespace DogeNews.Services.Common.Contracts
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
