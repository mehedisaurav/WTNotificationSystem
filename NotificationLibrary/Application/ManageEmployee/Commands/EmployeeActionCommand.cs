using Microsoft.EntityFrameworkCore;
using NotificationLibrary.Application.Common;
using NotificationLibrary.Application.ManageEmployee.Models;
using NotificationLibrary.NotificationServices;
using NotificationSystem.Infrastructure;
using NotificationSystem.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationLibrary.Application.ManageEmployee.Commands
{
    public class EmployeeActionCommand 
    {
        private readonly NotificationService _service;

        public EmployeeActionCommand(NotificationService service)
        {
            _service = service;
        }
        // Create/Update Employee
        public async void UpsertEmployee(EmployeeViewModel model)
        {
            _service.EmailService();
        }

        
    }
}
