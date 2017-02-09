namespace DogeNews.Web.Mvp.UserControls.NewsGrid.EventArguments
{
    public class OnArticleEditEventArgs
    {
        public bool IsAdminUser { get; set; }

        public string NewsItemId { get; set; }
    }
}