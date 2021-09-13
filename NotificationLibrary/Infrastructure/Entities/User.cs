using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationSystem.Infrastructure.Entities
{
    public class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NotificationPreferences { get; set; } 

        public virtual Employee Employee { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
