
using DogeNews.Services.Audit.Contracts;
using Ninject.Extensions.Interception;

namespace DogeNews.Web.Interception
{
    public class AdminActionsInterceptor : IInterceptor
    {
        private readonly IAdminActionAuditService adminActionAuditService;

        public AdminActionsInterceptor(IAdminActionAuditService adminActionAuditService)
        {
            this.adminActionAuditService = adminActionAuditService;
        }

        public void Intercept(IInvocation invocation)
        {
            this.adminActionAuditService.LogAdminActionToDatabase(invocation);
            invocation.Proceed();
        }
    }
}