using Microsoft.EntityFrameworkCore;
using NotificationLibrary.Infrastructure.Entities;
using NotificationSystem.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotificationSystem.Infrastructure
{
    public interface INotificationSystemDbContext
    {
        DbSet<Employee> Employees { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
        DbSet<Leave> Leaves { get; set; }

        Task<int> SaveChanges();
    }
}
