using System.Collections.Generic;
using piHome.Models.ValueObjects;

namespace piHome.Models.Dtos.UserSettings
{
    public class CircuitsHandlingSetDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
        public List<StateChange> StatesOnActivation { get; set; }
        public List<StateChange> StatesOnDeactivation { get; set; }
        public bool IsEnabled { get; set; }
    }
}