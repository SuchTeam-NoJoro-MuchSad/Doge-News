using System;
using Ninject.Extensions.Interception;

namespace DogeNews.Web.Interception
{
    public class ExceptionInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}