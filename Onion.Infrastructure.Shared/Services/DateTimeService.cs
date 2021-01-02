using $ext_projectname$.Application.Interfaces;
using System;

namespace $safeprojectname$.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
