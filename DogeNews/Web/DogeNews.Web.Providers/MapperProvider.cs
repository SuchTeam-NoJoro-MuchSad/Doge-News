using AutoMapper;

using DogeNews.Common.Attributes;
using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.Providers
{
    [InSingletonScope]
    public class MapperProvider : IMapperProvider
    {
        public IMapper Instance { get; set; }
    }
}
