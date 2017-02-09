using System.Threading;

using DogeNews.Common.Enums;
using DogeNews.Web.Services.Contracts;

namespace DogeNews.Web.Services
{
    public class NotificationsService : INotificationsService
    {
        public dynamic Clients { get; set; }

        public INotificationsService Toast(string message, int dellay, NotificationType type)
        {
            this.Clients.toast(message, dellay, type.ToString());
            return this;
        }

        public INotificationsService Sleep(int millesecondsTimeOut)
        {
            Thread.Sleep(millesecondsTimeOut);
            return this;
        }
    }
}
