using DogeNews.Common.Enums;

namespace DogeNews.Services.Common.Contracts
{
    public interface INotificationsService
    {
        dynamic Clients { get; set; }

        INotificationsService Toast(string message, int dellay, NotificationType type);

        INotificationsService Sleep(int millesecondsTimeOut);
    }
}
