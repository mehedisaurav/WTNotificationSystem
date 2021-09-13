using Microsoft.EntityFrameworkCore;
using NotificationLibrary.Application.Common;
using NotificationLibrary.Infrastructure.Entities;
using NotificationSystem.Infrastructure;
using NotificationSystem.NotificationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationLibrary.Application.ManageLeave.Commands
{
    public class LeaveManagementCommand
    {
        private readonly INotificationSystemDbContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly IWebPushService _webPushService;

        public LeaveManagementCommand() { }
        public LeaveManagementCommand(INotificationSystemDbContext dbContext,
                                        IEmailService emailService,
                                        ISmsService smsService,
                                        IWebPushService webPushService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
            _smsService = smsService;
            _webPushService = webPushService;
        }

        // Employee request for a leave
        public async void EmployeeLeaveRequest(int employeeId)
        {
            var employee = await _dbContext.Employees.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == employeeId);
            if (employee != null)
            {
                employee.Leaves.Add(new Leave
                {
                    DaysOfLeave = 1,
                    LeaveRequestDate = DateTime.Now,
                    LeaveStatus = LeaveProcessType.Pending,
                });

                await _dbContext.SaveChanges();

                // cheking preference
                if (employee.User.NotificationPreferences.Split(',').ToList().Contains(NotificationPreferencesType.Email.ToString()))
                {
                    // Find manager detail then send to manager email addrees
                    await _emailService.EmailSender("Manager email address", $"Dear Manager, Mr. {employee.FirstName} request for a leave.");
                }

                // cheking preference
                if (employee.User.NotificationPreferences.Split(',').ToList().Contains(NotificationPreferencesType.Sms.ToString()))
                {
                    // Find manager detail then send to manager sms
                    await _smsService.SmsSender("Manager phone number", $"Mr. {employee.FirstName} request for a leave.");
                }

                // cheking preference
                if (employee.User.NotificationPreferences.Split(',').ToList().Contains(NotificationPreferencesType.WebPush.ToString()))
                {
                    //Web push service
                }
            }
        }

        // Leave request approved/declined
        public async void LeaveApproval(int leaveId, int leaveApprovalType)
        {
            var leave = _dbContext.Leaves.Include(x => x.Employee)
                                         .ThenInclude(x => x.User)
                                         .FirstOrDefault(x => x.Id == leaveId && x.LeaveStatus == LeaveProcessType.Pending);

            if (leave != null)
            {
                leave.LeaveStatus = leaveApprovalType;
                leave.LeaveApprovalBy = "User Name";   //Approval By
                leave.LeaveApprovalDate = DateTime.Now;

                await _dbContext.SaveChanges();

                // cheking preference
                if (leave.Employee.User.NotificationPreferences.Split(',').ToList().Contains(NotificationPreferencesType.Email.ToString()))
                {
                    //Email notification send to employee for leave approval status
                    await _emailService.EmailSender(leave.Employee.Email,
                                                $"Dear {leave.Employee.FirstName}, Your leave request has been " +
                                                $"{(leaveApprovalType == LeaveProcessType.Approved ? "Approved" : "Declined")}.");
                }

                // cheking preference
                if (leave.Employee.User.NotificationPreferences.Split(',').ToList().Contains(NotificationPreferencesType.Sms.ToString()))
                {
                    //Sms notification send to employee for leave approval status
                    await _smsService.SmsSender(leave.Employee.PhoneNumber, $"Your leave request has been {(leaveApprovalType == LeaveProcessType.Approved ? "Approved" : "Declined")}.");
                }

                // cheking preference
                if (leave.Employee.User.NotificationPreferences.Split(',').ToList().Contains(NotificationPreferencesType.WebPush.ToString()))
                {
                    //Web push service
                }
            }
        }
    }
}
