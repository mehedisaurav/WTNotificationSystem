using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotificationSystem.NotificationServices
{
    public interface IWebPushService
    {
        Task BrowserPushNotificationSender();
    }
}
