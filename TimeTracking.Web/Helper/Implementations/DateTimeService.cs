using System;
using TimeTracking.Common.Interfaces;

namespace TimeTracking.Web.Helper.Implementations
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;

        public DateTime GetDifference(DateTime fromDateTime, DateTime toDateTime)
        {
            var difference = toDateTime.Date - fromDateTime.Date;
            return new DateTime(difference.Ticks);
        }
    }
}
