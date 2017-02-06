using System;
using System.Web;
using System.Reflection;
using System.Linq;

using Microsoft.Web.Infrastructure.DynamicModuleHelper;

using Ninject;
using Ninject.Web.Common;
using Ninject.Modules;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DogeNews.Web.Infrastructure.Bindings.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(DogeNews.Web.Infrastructure.Bindings.NinjectWebCommon), "Stop")]

namespace DogeNews.Web.Infrastructure.Bindings
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static IKernel Kernel { get; private set; }
        
        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            Kernel = kernel;

            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            var currentAssembly = Assembly.GetAssembly(typeof(NinjectWebCommon));
            var modules = currentAssembly
                .GetTypes()
                .Where(x => x.GetInterfaces().Any(i => i.Name == typeof(INinjectModule).Name))
                .Select(x => Activator.CreateInstance(x) as INinjectModule);

            kernel.Load(modules);
        }        
    }
}
