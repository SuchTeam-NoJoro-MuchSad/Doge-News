using System;
using System.Collections.Generic;
using System.Linq;

using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Ninject.Activation;

using WebFormsMvp.Binder;
using WebFormsMvp;

using DogeNews.Web.Infrastructure.Bindings.Factories.Contracts;
using DogeNews.Web.Infrastructure.Bindings.Factories;
using Ninject.Parameters;

namespace DogeNews.Web.Infrastructure.Bindings.Modules
{
    public class MvpModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IPresenterProvider>().ToFactory().InSingletonScope();
            this.Bind<IPresenterFactory>().To<WebFormsPresenterFactory>().InSingletonScope();
            this.Bind<IPresenter>()
                .ToMethod(this.PresenterMethod)
                .NamedLikeFactoryMethod((IPresenterProvider factory) => factory.GetPresenter(null, null, null));
        }

        private IPresenter PresenterMethod(IContext context)
        {
            List<IParameter> parameters = context.Parameters.ToList();

            // The presenter class type
            Type requestedType = parameters[0].GetValue(context, null) as Type;

            // The aspx.cs page type
            Type viewType = parameters[1].GetValue(context, null) as Type;

            // IWhateverView interface
            Type viewInterface = viewType.GetInterfaces().FirstOrDefault(i => i.Name.Contains("View") && !i.Name.Contains("IView"));

            // Instance of the aspx.cs page
            IView view = parameters[2].GetValue(context, null) as IView;

            this.BindInterface(viewInterface, view);
            return context.Kernel.Get(requestedType) as IPresenter;
        }

        private void BindInterface(Type viewInterface, IView view)
        {
            bool isInterfaceBinded = this.Kernel.GetBindings(viewInterface).Any();

            // After leaving the page the view gets destroyed, so the Model property
            // becomes null. The interface has to be rebinded.
            if (isInterfaceBinded)
            {
                this.Rebind(viewInterface).ToMethod(context => view);
                return;
            }

            // Bind the interface for the first time.
            this.Bind(viewInterface).ToMethod(context => view);
        }
    }
}