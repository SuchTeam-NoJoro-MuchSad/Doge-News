using AutoMapper;

namespace DogeNews.Web.Providers.Contracts
{
    public interface IMapperProvider
    {
        IMapper Instance { get; set; }

        MapperConfiguration Configuration { get; set; }
    }
}
