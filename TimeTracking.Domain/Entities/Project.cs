namespace TimeTracking.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public bool IsActive { get; set; }
    }
}