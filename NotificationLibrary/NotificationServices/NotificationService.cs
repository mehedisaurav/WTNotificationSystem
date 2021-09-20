using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotificationLibrary.NotificationServices
{
    public class NotificationService : INotificationService
    {
        public async override Task EmailService()
        {
            Console.WriteLine("");
        }

        public async override Task SmsService()
        {
            Console.WriteLine("");
        }

        public async override Task WebPushService()
        {
            Console.WriteLine("");
        }


    }
}
