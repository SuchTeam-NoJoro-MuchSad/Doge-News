namespace DogeNews.Services.Http.Contracts
{
    public interface IHttpResponseService
    {
        void Redirect(string url);

        void Clear();

        void SetStatusCode(int statusCode);

        void End();
    }
}
