using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotificationSystem.NotificationServices
{
    public class EmailService : IEmailService
    {
        public async Task EmailSender(string email, string body)
        {
            Console.WriteLine($"Dear User, {body}.");

            Console.WriteLine($"Email send to {email}");
        }
    }
}
