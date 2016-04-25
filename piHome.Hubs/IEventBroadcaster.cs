using piHome.Models.ValueObjects;

namespace piHome.Events
{
    public interface IEventBroadcaster
    {
        void BroadcastCircuitStateChange(StateChange change);
    }
}