using System.Collections.Generic;
using DogeNews.Web.Models;

namespace DogeNews.Web.Mvp.UserControls.AdminActionAudit
{
    public class AdminActionAuditViewModel
    {
        public IEnumerable<AdminActionLogWebModel> Logs { get; set; }
    }
}