using System.ComponentModel.DataAnnotations;

namespace TimeTracking.Domain.Enums
{
    public enum ActionType
    {
        [Display(Name = "TimeTracking.Start")]
        Start = 1,
        [Display(Name = "TimeTracking.End")]
        End = 2
    }
}
