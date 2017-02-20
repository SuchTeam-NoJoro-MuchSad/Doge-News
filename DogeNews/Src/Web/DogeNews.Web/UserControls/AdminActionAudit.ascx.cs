using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DogeNews.Web.Mvp.UserControls.AdminActionAudit;
using DogeNews.Web.Mvp.UserControls.AdminActionAudit.EventArguments;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace DogeNews.Web.UserControls
{
    [PresenterBinding(typeof(AdminActionAuditPresenter))]
    public partial class AdminActionAudit : MvpUserControl<AdminActionAuditViewModel>, IAdminActionAuditView
    {
        public event EventHandler<PageLoadEventArgs> PageLoad;

        protected void Page_Load(object sender, EventArgs e)
        {
            PageLoadEventArgs eventArgs = new PageLoadEventArgs();
            this.PageLoad(this, eventArgs);
        }
    }
}