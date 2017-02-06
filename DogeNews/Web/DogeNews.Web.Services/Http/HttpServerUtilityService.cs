using System.Web;

using DogeNews.Web.Services.Contracts.Http;

namespace DogeNews.Web.Services.Http
{
    public class HttpServerUtilityService : IHttpServerUtilityService
    {
        public string MapPath(string path)
        {
            string mappedPath = HttpContext.Current.Server.MapPath(path);
            return mappedPath;
        }
    }
}
