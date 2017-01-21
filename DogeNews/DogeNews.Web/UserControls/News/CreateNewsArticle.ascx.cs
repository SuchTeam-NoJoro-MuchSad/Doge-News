using System;

namespace DogeNews.Web.UserControls.News
{
    public partial class CreateNewsArticle : System.Web.UI.UserControl
    {
        public string Content
        {
            get { return this.TinymceTextarea.InnerText; }
        }
    }
}