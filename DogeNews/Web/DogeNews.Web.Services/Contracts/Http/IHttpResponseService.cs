using System.Web;

namespace DogeNews.Web.Services.Contracts.Http
{
    public interface IHttpResponseService
    {
        void Clear();

        void SetStatusCode(int statusCode);

        void End();
    }
}
