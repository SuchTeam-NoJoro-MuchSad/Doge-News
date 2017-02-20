using System;
using DogeNews.Web.Mvp.UserControls.AdminActionAudit.EventArguments;
using WebFormsMvp;

namespace DogeNews.Web.Mvp.UserControls.AdminActionAudit
{
    public interface IAdminActionAuditView : IView<AdminActionAuditViewModel>
    {
        event EventHandler<PageLoadEventArgs> PageLoad;
    }
}