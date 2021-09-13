using Microsoft.EntityFrameworkCore;
using NotificationLibrary.Infrastructure.Entities;
using NotificationSystem.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotificationSystem.Infrastructure
{
    public class NotificationSystemDbContext : DbContext, INotificationSystemDbContext
    {
        public NotificationSystemDbContext()
        {
            //configure db connection by DbContextOptions
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Leave> Leaves { get; set; }

        public async Task<int> SaveChanges()
        {
            throw new NotSupportedException();
        }
    }
}
