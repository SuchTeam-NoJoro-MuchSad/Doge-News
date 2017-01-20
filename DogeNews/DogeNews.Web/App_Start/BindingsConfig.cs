using Ninject;
using WebFormsMvp.Binder;

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