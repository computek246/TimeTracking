using TimeTracking.Common.Models.Interfaces;

namespace TimeTracking.Common.Models
{
    public class BaseEntity : IEntity<int>
    {
        public int Id { get; set; } // Id (Primary key)
    }
}