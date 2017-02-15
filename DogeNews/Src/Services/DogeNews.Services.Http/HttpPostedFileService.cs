using System.Web;

using DogeNews.Services.Http.Contracts;

namespace DogeNews.Services.Http
{
    public class HttpPostedFileService : IHttpPostedFileService
    {
        public void SaveAs(HttpPostedFile file, string fileName)
        {
            file.SaveAs(fileName);
        }
    }
}
