using System.Web;

using DogeNews.Web.Services.Contracts.Http;

namespace DogeNews.Web.Services.Http
{
    public class HttpResponseService : IHttpResponseService
    {
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
