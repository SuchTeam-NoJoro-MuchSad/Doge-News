using System.Threading;

using DogeNews.Common.Enums;
using DogeNews.Common.Attributes;
using DogeNews.Services.Common.Contracts;

namespace DogeNews.Services.Common
{
    [InSingletonScope]
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
