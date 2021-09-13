using NotificationSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotificationLibrary.Application.ManageMyProfile.Commands
{
    public class MyProfileSetCommand
    {
        private readonly INotificationSystemDbContext _dbContext;

        public MyProfileSetCommand(){}
        public MyProfileSetCommand(INotificationSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SetNotificationPreference(int userId, int[] preferences)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user != null)
            {
                user.NotificationPreferences = String.Join(",", preferences);

                await _dbContext.SaveChanges();
            }
        }
    }
}
