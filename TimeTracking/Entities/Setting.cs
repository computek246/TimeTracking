namespace TimeTracking.Entities
{
    public class Setting
    {
        public Setting() { }

        public Setting(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
