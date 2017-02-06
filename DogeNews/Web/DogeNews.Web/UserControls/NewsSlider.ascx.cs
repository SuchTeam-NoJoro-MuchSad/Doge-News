using System.Collections.Generic;
using System.Web.UI;

using DogeNews.Web.Models;

namespace DogeNews.Web.UserControls
{
    public partial class NewsSlider : UserControl
    {
        public IEnumerable<NewsWebModel> News { get; set; }
    }
}