using DogeNews.Services.Common.Contracts;
using DogeNews.Web.Infrastructure.Bindings;

using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

using Ninject;

namespace DogeNews.Web.Hubs
{
    [HubName("notificationHub")]
    public class NotificationHub : Hub
    {
        public void Init()
        {
            INotificationsService notificationsService = NinjectWebCommon.Kernel.Get<INotificationsService>();
            notificationsService.Clients = this.Clients.All;
        }
    }
}