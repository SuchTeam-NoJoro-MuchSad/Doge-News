using AutoMapper;
using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.Providers
{
    public class MapperProvider : IMapperProvider
    {
        public IMapper Instance { get; set; }
    }
}
