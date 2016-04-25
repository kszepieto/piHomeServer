using Microsoft.AspNet.SignalR;
using piHome.Events.Hubs;
using piHome.Models.ValueObjects;

namespace piHome.Events
{
    public class EventBroadcaster : IEventBroadcaster
    {
        public void BroadcastCircuitStateChange(StateChange change)
        {
            var clients = GlobalHost.ConnectionManager.GetHubContext<PiHomeHub>().Clients;

            clients.All.CircuitStateChanged(change);
        }
    }
}
