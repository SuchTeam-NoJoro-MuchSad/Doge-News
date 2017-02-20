using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.Mvp.UserControls.AdminActionAudit.EventArguments;

using WebFormsMvp;

namespace DogeNews.Web.Mvp.UserControls.AdminActionAudit
{
    public class AdminActionAuditPresenter : Presenter<IAdminActionAuditView>
    {
        private IProjectableRepository<AdminActionLog> logsProjectableRepository;

        public AdminActionAuditPresenter(IAdminActionAuditView view,
            IProjectableRepository<AdminActionLog> logsProjectableRepository) : base(view)
        {
            this.logsProjectableRepository = logsProjectableRepository;

            this.View.PageLoad += LoadLogs;
        }

        public void LoadLogs(object sender, PageLoadEventArgs eventArgs)
        {
            this.View.Model.Logs = this.logsProjectableRepository.GetAllMapped<AdminActionLogWebModel>();
        }
    }
}