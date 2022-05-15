using System.Collections.Generic;

namespace TimeTracking.Domain.Entities
{
    public class Project
    {
        public Project()
        {
            ActionsLogs = new HashSet<ActionsLog>();
        }

        public int Id { get; set; }
        public string ProjectName { get; set; }
        public bool IsActive { get; set; }

        public ICollection<ActionsLog> ActionsLogs { get; set; }
    }
}