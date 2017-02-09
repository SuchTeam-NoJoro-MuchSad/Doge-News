using DogeNews.Web.Infrastructure.Bindings;
using DogeNews.Web.Services.Contracts;

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
            var notificationsService = NinjectWebCommon.Kernel.Get<INotificationsService>();
            notificationsService.Clients = this.Clients.All;
        }
    }
}