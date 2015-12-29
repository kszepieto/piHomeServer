using piHome.Models;

namespace piHome.Events
{
    public interface IEventBroadcaster
    {
        void BroadcastCircuitStateChange(CircuitStateChange change);
    }
}