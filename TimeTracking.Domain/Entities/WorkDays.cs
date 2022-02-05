using System;

namespace TimeTracking.Domain.Entities
{
    public class WorkDays
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
        public int TotalMinute { get; set; }
    }
}
