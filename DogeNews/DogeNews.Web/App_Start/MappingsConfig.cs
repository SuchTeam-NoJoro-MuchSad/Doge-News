using AutoMapper;

using DogeNews.Data;
using DogeNews.Web.Providers;

namespace DogeNews.Web.App_Start
{
    public class MappingsConfig
    {
        public static void Map()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataMappingsProfile>();
            });

            var mapper = config.CreateMapper();
            MapperProvider.Instance = mapper;
        }
    }
}