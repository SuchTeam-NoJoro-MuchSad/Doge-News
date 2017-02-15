using System.Web;

namespace DogeNews.Services.Http.Contracts
{
    public interface IHttpPostedFileService
    {
        void SaveAs(HttpPostedFile file, string fileName);
    }
}
