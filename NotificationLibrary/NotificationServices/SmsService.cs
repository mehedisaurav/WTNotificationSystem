using NotificationSystem.NotificationServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotificationLibrary.NotificationServices
{
    public class SmsService : ISmsService
    {
        public async Task SmsSender(string phone, string sms)
        {
            Console.WriteLine($"Sms send to {phone} for user name {sms}");
        }
    }
}
