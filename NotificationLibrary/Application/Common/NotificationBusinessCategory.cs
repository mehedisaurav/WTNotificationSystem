using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationLibrary.Application.Common
{
    public static class NotificationBusinessCategory
    {
    }

    public static class LeaveProcessType
    {
        public const int Approved = 101;
        public const int Declined = 102;
        public const int Pending = 103; 
    }

    public static class NotificationPreferencesType 
    {
        public const int Email = 201;
        public const int Sms = 202;
        public const int WebPush = 203; 
    }
}
