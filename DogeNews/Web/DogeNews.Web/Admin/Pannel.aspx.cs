using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DogeNews.Web.Admin
{
    public partial class Pannel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.User.IsInRole(Common.Constants.Roles.Admin))
            {
                this.Context.Response.Redirect("/");
            }
        }
    }
}