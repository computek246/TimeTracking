using System;

namespace TimeTracking.Domain.Entities
{
    public class ActionsLog
    {
        public int Id { get; set; }
        public int? ActionId { get; set; }
        public string ActionName { get; set; }
        public DateTime ActionDate { get; set; }
        public int? ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
