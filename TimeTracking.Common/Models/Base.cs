using TimeTracking.Common.Models.Interfaces;

namespace TimeTracking.Common.Models
{
    public class Base : Auditable<int?>, IBase<int>
    {
        public string Name { get; set; }
    }
}