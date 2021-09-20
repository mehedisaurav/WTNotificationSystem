using NotificationLibrary.Application.Common;
using NotificationLibrary.Application.ManageEmployee.Commands;
using NotificationLibrary.Application.ManageEmployee.Models;
using NotificationLibrary.Application.ManageLeave.Commands;
using NotificationLibrary.Application.ManageMyProfile.Commands;
using NotificationLibrary.NotificationServices;
using NotificationSystem.Infrastructure;
using System;

namespace NotificationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WT Notification System");

            #region Manage Employee
            EmployeeActionCommand employeeAction = new EmployeeActionCommand(new NotificationService());

            employeeAction.UpsertEmployee(new EmployeeViewModel
            {
                Id = 0,
                FirstName = "Mehedi",
                LastName = "Islam",
                PhoneNumber = "016724611",
                Email = "mehedisaurav@gmail.com",
                UserId = 1,
                ManagerId = 1
            });

            //Consider already exist an employee into employee table
            //update an employee detail
            employeeAction.UpsertEmployee(new EmployeeViewModel
            {
                Id = 1,
                FirstName = "Mehedi",
                LastName = "Islam",
                PhoneNumber = "01672461170",
                Email = "mehedisaurav@gmail.com",
                UserId = 1,
                ManagerId = 1
            });


            #endregion

            #region Manage Leave
            LeaveManagementCommand leaveManagement = new LeaveManagementCommand();
            //request for a leave(Leave Create)
            leaveManagement.EmployeeLeaveRequest(1);

            //approval for leave
            leaveManagement.LeaveApproval(1, LeaveProcessType.Approved);
            #endregion

            #region My Profile Preference
            MyProfileSetCommand profileSetCommand = new MyProfileSetCommand();
            
            //Set Preference for a user
            profileSetCommand.SetNotificationPreference(userId: 1, new int[] { NotificationPreferencesType.Email, NotificationPreferencesType.Sms });

            #endregion

        }
    }
}
