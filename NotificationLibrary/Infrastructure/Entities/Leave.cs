using NotificationSystem.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationLibrary.Infrastructure.Entities
{
    public class Leave
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime LeaveRequestDate { get; set; }
        public int DaysOfLeave { get; set; }
        public int LeaveStatus { get; set; }
        public string LeaveApprovalBy { get; set; }
        public DateTime LeaveApprovalDate { get; set; } 

        public virtual Employee Employee { get; set; } 
    }
}
