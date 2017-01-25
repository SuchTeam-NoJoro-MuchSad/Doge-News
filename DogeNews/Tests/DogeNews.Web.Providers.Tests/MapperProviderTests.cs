using System;
using AutoMapper;
using DogeNews.Web.Providers.Config;
using NUnit.Framework;

namespace DogeNews.Web.Providers.Tests
{
    [TestFixture]
    public class MapperProviderTests
    {
        [Test]
        public void Instance_ShouldBeOfTypeMapper()
        {
            var mapperProvider = new MapperProvider();
            //Assume ninject did it's magic
            mapperProvider.Instance = new Mapper(new MapperConfiguration(delegate(IMapperConfigurationExpression expression) {  }));

            var mapperProviderInstance = mapperProvider.Instance;
            
            Assert.AreEqual(mapperProviderInstance.GetType(), typeof(Mapper));
        }
    }
}