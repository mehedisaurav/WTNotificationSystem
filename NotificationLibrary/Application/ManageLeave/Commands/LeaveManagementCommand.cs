using Microsoft.EntityFrameworkCore;
using NotificationLibrary.Application.Common;
using NotificationLibrary.Infrastructure.Entities;
using NotificationLibrary.NotificationServices;
using NotificationSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationLibrary.Application.ManageLeave.Commands
{
    public class LeaveManagementCommand
    {
        private readonly INotificationSystemDbContext _dbContext;
        private readonly NotificationService _service;

        public LeaveManagementCommand()
        {

        }
        public LeaveManagementCommand(INotificationSystemDbContext dbContext, NotificationService service)
        {
            _dbContext = dbContext;
            _service = service;
        }

        // Employee request for a leave
        public async Task EmployeeLeaveRequest(int employeeId)
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
                    //await _emailService.EmailSender("Manager email address", $"Dear Manager, Mr. {employee.FirstName} request for a leave.");
                     await _service.EmailService();
                }

                // cheking preference
                if (employee.User.NotificationPreferences.Split(',').ToList().Contains(NotificationPreferencesType.Sms.ToString()))
                {
                    // Find manager detail then send to manager sms
                    //await _smsService.SmsSender("Manager phone number", $"Mr. {employee.FirstName} request for a leave.");
                    await _service.SmsService();
                }

                // cheking preference
                if (employee.User.NotificationPreferences.Split(',').ToList().Contains(NotificationPreferencesType.WebPush.ToString()))
                {
                    //Web push service
                    await _service.WebPushService(); 
                }
            }
        }

        // Leave request approved/declined
        public async Task LeaveApproval(int leaveId, int leaveApprovalType)
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
                    //await _emailService.EmailSender(leave.Employee.Email,
                    //                            $"Dear {leave.Employee.FirstName}, Your leave request has been " +
                    //                            $"{(leaveApprovalType == LeaveProcessType.Approved ? "Approved" : "Declined")}.");
                    await _service.EmailService();
                }

                // cheking preference
                if (leave.Employee.User.NotificationPreferences.Split(',').ToList().Contains(NotificationPreferencesType.Sms.ToString()))
                {
                    //Sms notification send to employee for leave approval status
                    //await _smsService.SmsSender(leave.Employee.PhoneNumber, $"Your leave request has been {(leaveApprovalType == LeaveProcessType.Approved ? "Approved" : "Declined")}.");
                    await _service.SmsService();
                }

                // cheking preference
                if (leave.Employee.User.NotificationPreferences.Split(',').ToList().Contains(NotificationPreferencesType.WebPush.ToString()))
                {
                    //Web push service
                    await _service.WebPushService();
                }
            }
        }
    }
}
