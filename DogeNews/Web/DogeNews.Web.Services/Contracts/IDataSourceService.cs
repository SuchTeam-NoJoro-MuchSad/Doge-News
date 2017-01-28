using System.Collections.Generic;

namespace DogeNews.Web.Services.Contracts
{
    public interface IDataSourceService<DbModelType, TargetMapType> where DbModelType : class
    {
        int Count { get; }

        IEnumerable<TargetMapType> GetPageItems(int page, int pageSize);
    }
}
