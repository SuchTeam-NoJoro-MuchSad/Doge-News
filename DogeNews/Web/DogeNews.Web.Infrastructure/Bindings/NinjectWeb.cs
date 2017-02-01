using Microsoft.Web.Infrastructure.DynamicModuleHelper;

using Ninject.Web;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DogeNews.Web.Infrastructure.Binding.NinjectWeb), "Start")]

namespace DogeNews.Web.Infrastructure.Binding
{
    public static class NinjectWeb 
    {
        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
        }
    }
}
