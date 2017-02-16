using System;
using System.Web;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Web.Infrastructure.DynamicModuleHelper;

using Ninject;
using Ninject.Web.Common;
using Ninject.Extensions.Interception.Infrastructure.Language;

using DogeNews.Common.Constants;
using DogeNews.Web.Infrastructure.Bindings.Modules;
using DogeNews.Data.Contracts;
using DogeNews.Data.Repositories;
using DogeNews.Web.DataSources.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.DataSources;
using DogeNews.Common.Attributes;
using DogeNews.Common.Extension;
using Ninject.Syntax;

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
            StandardKernel kernel = new StandardKernel();
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
            IEnumerable<Assembly> assemblies = GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                IEnumerable<Type> types = assembly.GetTypes().Where(t => t.IsClass && !t.IsGenericType);

                foreach (Type type in types)
                {
                    Type defaultInterface = type
                        .GetInterfaces()
                        .FirstOrDefault(i => i.Name == $"I{type.Name}");
                    bool isInSingletonScope = Attribute
                        .GetCustomAttribute(type, typeof(InSingletonScopeAttribute)) != null;
                    bool isInrequestScope = Attribute
                        .GetCustomAttribute(type, typeof(InRequestScopeAttribute)) != null;
                    bool isInterceptable = Attribute
                        .GetCustomAttribute(type, typeof(InterceptableAttribute)) != null;

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

                    if (isInterceptable)
                    {
                        IList<Type> interceptors = type
                            .GetAttributeValues((InterceptableAttribute atr) => atr.TypeOfInterceptors);

                        IBindingWhenInNamedWithOrOnSyntax<object> binding = kernel
                            .Bind(defaultInterface)
                            .To(type);

                        for (int i = 0; i < interceptors.Count; i++)
                        {
                            binding
                                .Intercept()
                                .With(interceptors[i]).InOrder(i + 1); // order is 1 based
                        }

                        continue;
                    }

                    kernel.Bind(defaultInterface).To(type);
                }
            }

            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();
            kernel
                .Bind(typeof(IProjectableRepository<>))
                .To(typeof(ProjectableRepository<>))
                .InRequestScope();
            kernel.Bind<INewsDataSource<NewsItem, NewsWebModel>>()
                .To<NewsDataSource>()
                .InRequestScope();

            kernel.Load(new MvpModule());
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            List<Assembly> assemblies = new List<Assembly>
            {
                Assembly.Load(ServerConstants.DataAssembly),
                Assembly.Load(ServerConstants.ServicesCommonAssembly),
                Assembly.Load(ServerConstants.ServicesDataAssembly),
                Assembly.Load(ServerConstants.ServicesHttpAssembly),
            };

            return assemblies;
        }
    }
}
