using System;

using WebFormsMvp;

namespace DogeNews.Web.Infrastructure.Bindings.Factories.Contracts
{
    public interface IPresenterProvider
    {
        IPresenter GetPresenter(Type presenterType, Type viewType, IView viewInstance);
    }
}
