using System;

using WebFormsMvp;

namespace DogeNews.Web.Factories.Contracts
{
    public interface IPresenterProvider
    {
        IPresenter GetPresenter(Type presenterType, Type viewType, IView viewInstance);
    }
}
