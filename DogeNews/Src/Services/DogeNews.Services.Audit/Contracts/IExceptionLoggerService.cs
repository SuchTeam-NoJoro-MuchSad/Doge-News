using System;
using DogeNews.Common.Enums;

namespace DogeNews.Services.Audit.Contracts
{
    public interface IExceptionLoggerService
    {
        void Log(LoggerSeverityLogLevelType logLevel, Exception exception);
    }
}