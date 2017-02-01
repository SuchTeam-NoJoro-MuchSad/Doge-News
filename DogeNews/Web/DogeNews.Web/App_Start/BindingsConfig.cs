using Ninject;

using WebFormsMvp.Binder;

using DogeNews.Web.Infrastructure.Bindings;

namespace DogeNews.Web.App_Start
{
    public class BindingsConfig
    {
        public static void BindPresenterFactory()
        {
            var presenterFactory = NinjectWebCommon.Kernel.Get<IPresenterFactory>();
            PresenterBinder.Factory = presenterFactory;
        }
    }
}