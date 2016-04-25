using piHome.Models.Enums;

namespace piHome.WebHost.WebModels.Circuits
{
    public class StateChangeVM
    {
        public Circuit Circuit { get; set; }
        public bool NewState { get; set; }
    }
}
