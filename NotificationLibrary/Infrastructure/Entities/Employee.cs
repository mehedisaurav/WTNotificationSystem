using NotificationLibrary.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationSystem.Infrastructure.Entities
{
    public class Employee
    {
        public Employee()
        {
            Leaves = new HashSet<Leave>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int ManagerId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Leave> Leaves{ get; set; }
    }
}
