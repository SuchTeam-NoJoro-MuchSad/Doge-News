using System;
using System.Text;
using DogeNews.Common.Enums;
using DogeNews.Services.Audit.Contracts;

using NLog;

namespace DogeNews.Services.Audit
{
    class ExceptionLoggerService : IExceptionLoggerService
    {
        private ILogger logger;

        public ExceptionLoggerService(ILogger logger)
        {
            this.logger = logger;
        }

        public void Log(LoggerSeverityLogLevelType logLevel, Exception exception)
        {
            var message = new StringBuilder();
            message.AppendLine($"Exception Message: {exception.Message}");
            message.AppendLine($"Exception Sourse: {exception.Source}");
            message.AppendLine($"Exception Stack trace: {exception.StackTrace}");

            if (logLevel == LoggerSeverityLogLevelType.Debug)
            {
                this.logger.Debug(exception, message.ToString());
            }
            else if (logLevel == LoggerSeverityLogLevelType.Info)
            {
                this.logger.Info(exception, message.ToString());
            }
            else if (logLevel == LoggerSeverityLogLevelType.Warn)
            {
                this.logger.Warn(exception, message.ToString());
            }
            else if (logLevel == LoggerSeverityLogLevelType.Fatal)
            {
                this.logger.Fatal(exception,message.ToString());
            }
        }
    }
}
