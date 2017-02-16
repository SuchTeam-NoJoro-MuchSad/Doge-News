using DogeNews.Web.Infrastructure.Bindings;
using Ninject;
using WebFormsMvp.Binder;

namespace DogeNews.Web
{
    public class BindingsConfig
    {
        public static void BindPresenterFactory()
        {
            IPresenterFactory presenterFactory = NinjectWebCommon.Kernel.Get<IPresenterFactory>();
            PresenterBinder.Factory = presenterFactory;
        }
    }
}