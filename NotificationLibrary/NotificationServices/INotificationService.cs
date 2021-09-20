using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotificationLibrary.NotificationServices
{
    public abstract class INotificationService
    {
        public abstract Task EmailService();

        public abstract Task SmsService();

        public abstract Task WebPushService();
    }
}
