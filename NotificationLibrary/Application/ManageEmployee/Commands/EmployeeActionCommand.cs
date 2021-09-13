using Microsoft.EntityFrameworkCore;
using NotificationLibrary.Application.Common;
using NotificationLibrary.Application.ManageEmployee.Models;
using NotificationSystem.Infrastructure;
using NotificationSystem.Infrastructure.Entities;
using NotificationSystem.NotificationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationLibrary.Application.ManageEmployee.Commands
{
    public class EmployeeActionCommand
    {
        private readonly INotificationSystemDbContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly IWebPushService _webPushService;

        public EmployeeActionCommand()
        {

        }
        public EmployeeActionCommand(INotificationSystemDbContext dbContext, 
                                        IEmailService emailService,
                                        ISmsService smsService,
                                        IWebPushService webPushService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
            _smsService = smsService;
            _webPushService = webPushService;
        }

        // Create/Update Employee
        public async void UpsertEmployee(EmployeeViewModel model)
        {
            if(model.Id == 0) //Create new employee
            {
                _dbContext.Employees.Add(new Employee
                {
                    FirstName = model.FirstName.Trim(),
                    LastName = model.LastName.Trim(),
                    PhoneNumber = model.PhoneNumber.Trim(),
                    Email = model.Email.Trim(),
                    UserId = model.UserId,
                    ManagerId = model.ManagerId,
                    Id = 1
                });
                _dbContext.Users.Add(new User
                {
                    Id = 1,
                    UserName = "Mehedi",
                    Password = "0000"
                });
            }
            else //update employee
            {
                Employee entity = await _dbContext.Employees.Include(x=>x.User).FirstOrDefaultAsync(x=>x.Id == model.Id);
                if(entity != null)
                {
                    entity.FirstName = model.FirstName.Trim();
                    entity.LastName = model.LastName.Trim();
                    entity.PhoneNumber = model.PhoneNumber.Trim();
                    entity.UserId = model.UserId;
                    entity.Email = model.Email;
                    entity.ManagerId = model.ManagerId;

                    //cheking preference from my profile set
                    if (entity.User.NotificationPreferences.Split(',').ToList().Contains(NotificationPreferencesType.Email.ToString()))
                    {
                        // Email notification Sending
                        await _emailService.EmailSender(entity.Email, $"Dear {entity.FirstName} {entity.LastName}, Your profile has been updated.");
                    }

                    //cheking preference from my profile set
                    if (entity.User.NotificationPreferences.Split(',').ToList().Contains(NotificationPreferencesType.Sms.ToString()))
                    {
                        // Sms notification Sending
                        await _smsService.SmsSender(entity.PhoneNumber, $"{entity.FirstName} {entity.LastName}");
                    }

                }
            }

            await _dbContext.SaveChanges();
        }

        
    }
}
