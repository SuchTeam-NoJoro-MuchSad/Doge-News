using System;
using System.Web;
using DogeNews.Common.Enums;
using DogeNews.Services.Audit.Contracts;
using DogeNews.Services.Http.Contracts;
using Ninject.Extensions.Interception;

namespace DogeNews.Web.Interception
{
    public class ExceptionInterceptor : IInterceptor
    {
        private IExceptionLoggerService exceptionLogger;
        private readonly IHttpResponseService httpResponseService;

        public ExceptionInterceptor(IExceptionLoggerService exceptionLogger,
            IHttpResponseService httpResponseService)
        {
            this.exceptionLogger = exceptionLogger;
            this.httpResponseService = httpResponseService;
        }
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception exception)
            {
                exceptionLogger.Log(LoggerSeverityLogLevelType.Fatal, exception);
                this.httpResponseService.Redirect("~/Errors/DefaultErrorPage.aspx");
            }
        }
    }
}