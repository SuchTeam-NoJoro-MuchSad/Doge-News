using System.Web;

namespace DogeNews.Web.Services.Contracts.Http
{
    public interface IHttpPostedFileService
    {
        void SaveAs(HttpPostedFile file, string fileName);
    }
}
