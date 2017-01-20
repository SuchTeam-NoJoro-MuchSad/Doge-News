using AutoMapper;

namespace DogeNews.Web.Providers
{
    public class MapperProvider
    {
        private static volatile IMapper instance;
        private static object lockObject = new object();

        private MapperProvider()
        {
        }

        public static IMapper Instance
        {
            get
            {
                return instance;
            }

            set
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = value;
                        }
                    }
                }
            }
        }
    }
}
