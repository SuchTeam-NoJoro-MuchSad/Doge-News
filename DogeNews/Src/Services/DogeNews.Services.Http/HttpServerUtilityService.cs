using System.Web;

using DogeNews.Services.Http.Contracts;

namespace DogeNews.Services.Http
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
