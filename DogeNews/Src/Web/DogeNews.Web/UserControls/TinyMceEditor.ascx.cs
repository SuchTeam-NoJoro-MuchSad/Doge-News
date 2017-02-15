using System.Web.UI;

namespace DogeNews.Web.UserControls
{
    public partial class TinyMceEditor : UserControl
    {
        public string Content
        {
            get { return this.TinymceTextarea.InnerText; }
            set { this.TinymceTextarea.InnerText = value; }
        }
    }
}