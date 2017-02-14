using System;
using System.Web;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Web.Infrastructure.DynamicModuleHelper;

using Ninject;
using Ninject.Web.Common;

using DogeNews.Common.Constants;
using DogeNews.Web.Infrastructure.Bindings.Modules;
using DogeNews.Data.Contracts;
using DogeNews.Data.Repositories;
using DogeNews.Web.DataSources.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.DataSources;
using DogeNews.Common.Attributes;

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
            var assemblies = GetAssemblies();

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes().Where(t => t.IsClass && !t.IsGenericType);

                foreach (var type in types)
                {
                    var defaultInterface = type
                        .GetInterfaces()
                        .FirstOrDefault(i => i.Name == $"I{type.Name}");
                    var isInSingletonScope = Attribute
                        .GetCustomAttribute(type, typeof(InSingletonScopeAttribute)) != null;
                    var isInrequestScope = Attribute
                        .GetCustomAttribute(type, typeof(InRequestScopeAttribute)) != null;

                    if (defaultInterface == null)
                    {
                        continue;
                    }

                    if (isInSingletonScope)
                    {
                        kernel.Bind(defaultInterface).To(type).InSingletonScope();
                        continue;
                    }

                    if (isInrequestScope)
                    {
                        kernel.Bind(defaultInterface).To(type).InRequestScope();
                        continue;
                    }

                    kernel.Bind(defaultInterface).To(type);
                }
            }

            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();
            kernel.Bind<INewsDataSource<NewsItem, NewsWebModel>>()
                .To<NewsDataSource>()
                .InRequestScope();

            kernel.Load(new MvpModule());
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            var assemblies = new List<Assembly>
            {
                Assembly.Load(ServerConstants.DataAssembly),
                Assembly.Load(ServerConstants.ServicesAssembly),
                Assembly.Load(ServerConstants.ProvidersAssembly)
            };

            return assemblies;
        }
    }
}
