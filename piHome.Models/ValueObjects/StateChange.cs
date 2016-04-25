namespace piHome.Models.ValueObjects
{
    public class StateChange
    {
        public Enums.Circuit Circuit { get; set; }
        public bool State { get; set; }
    }
}