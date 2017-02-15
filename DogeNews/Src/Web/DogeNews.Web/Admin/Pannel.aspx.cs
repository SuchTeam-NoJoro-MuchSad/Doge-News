using System;
using System.Web.UI;

using DogeNews.Common.Constants;

namespace DogeNews.Web.Admin
{
    public partial class Pannel : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.User.IsInRole(Roles.Admin))
            {
                this.Context.Response.Redirect("/");
            }
        }
    }
}