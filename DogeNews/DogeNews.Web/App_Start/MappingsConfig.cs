using AutoMapper;

using DogeNews.Data;
using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.App_Start
{
    public class MappingsConfig
    {
        private readonly IMapperProvider mapperProvider;

        public MappingsConfig(IMapperProvider mapperProvider)
        {
            this.mapperProvider = mapperProvider;
        }

        public void Map()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataMappingsProfile>();
            });

            var mapper = config.CreateMapper();
            mapperProvider.Instance = mapper;
        }
    }
}