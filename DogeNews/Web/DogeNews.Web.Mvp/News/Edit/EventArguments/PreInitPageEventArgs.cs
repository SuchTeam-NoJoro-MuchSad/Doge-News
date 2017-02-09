namespace DogeNews.Web.Mvp.News.Edit.EventArguments
{
    public class PreInitPageEventArgs
    {
        public string QueryString { get; set; }

        public bool IsAdminUser { get; set; }
    }
}