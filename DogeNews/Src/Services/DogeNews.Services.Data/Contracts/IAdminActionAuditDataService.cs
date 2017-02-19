using Ninject.Extensions.Interception;

namespace DogeNews.Services.Data.Contracts
{
    public interface IAdminActionAuditDataService
    {
        void LogAdminActionToDatabase(IInvocation invocation);
    }
}