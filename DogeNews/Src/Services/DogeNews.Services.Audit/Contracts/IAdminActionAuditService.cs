using System.Collections.Generic;
using DogeNews.Data.Models;
using Ninject.Extensions.Interception;

namespace DogeNews.Services.Audit.Contracts
{
    public interface IAdminActionAuditService
    {
        void LogAdminActionToDatabase(IInvocation invocation);

        IEnumerable<AdminActionLog> GetAllActionLogs();
    }
}