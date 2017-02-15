using System.Web;

using DogeNews.Services.Http.Contracts;

namespace DogeNews.Services.Http
{
    public class HttpResponseService : IHttpResponseService
    {
        public void Redirect(string url)
        {
            HttpContext.Current.Response.Redirect(url);
        }

        public void Clear()
        {
            HttpContext.Current.Response.Clear();
        }

        public void End()
        {
            HttpContext.Current.Response.End();
        }

        public void SetStatusCode(int statusCode)
        {
            HttpContext.Current.Response.StatusCode = statusCode;
        }
    }
}
