using System.Collections.Generic;
using DogeNews.Data.Models;
using DogeNews.Web.Models;

namespace DogeNews.Web.MVP.UserControls.ArticleComments
{
    public class ArticleCommentsViewModel
    {
        public IEnumerable<CommentWebModel> Comments { get; set; }
    }
}