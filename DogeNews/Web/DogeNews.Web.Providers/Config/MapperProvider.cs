using AutoMapper;
using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.Providers.Config
{
    public class MapperProvider : IMapperProvider
    {
        public IMapper Instance { get; set; }
    }
}
