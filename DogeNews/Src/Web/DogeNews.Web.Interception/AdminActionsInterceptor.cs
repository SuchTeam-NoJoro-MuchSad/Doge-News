using Ninject.Extensions.Interception;

namespace DogeNews.Web.Interception
{
    public class AdminActionsInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
           invocation.Proceed();
        }
    }
}