using Ninject.Extensions.Interception;

namespace DogeNews.Services.Audit.Contracts
{
    public interface IAdminActionAuditService
    {
        void LogAdminActionToDatabase(IInvocation invocation);
    }
}