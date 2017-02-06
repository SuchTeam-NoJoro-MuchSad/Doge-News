using System.Web;

using DogeNews.Web.Services.Contracts.Http;

namespace DogeNews.Web.Services.Http
{
    public class HttpPostedFileService : IHttpPostedFileService
    {
        public void SaveAs(HttpPostedFile file, string fileName)
        {
            file.SaveAs(fileName);
        }
    }
}
