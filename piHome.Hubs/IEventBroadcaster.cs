using piHome.Models.Circuit;

namespace piHome.Events
{
    public interface IEventBroadcaster
    {
        void BroadcastCircuitStateChange(StateChange change);
    }
}