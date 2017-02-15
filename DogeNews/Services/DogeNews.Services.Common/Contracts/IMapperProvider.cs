using AutoMapper;

namespace DogeNews.Services.Common.Contracts
{
    public interface IMapperProvider
    {
        IMapper Instance { get; set; }

        IConfigurationProvider Configuration { get; set; }
    }
}
