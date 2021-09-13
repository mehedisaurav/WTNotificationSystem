using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationLibrary.Application.ManageEmployee.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int ManagerId { get; set; }
    }
}
