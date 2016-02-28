using Microsoft.AspNet.SignalR;
using piHome.Events.Hubs;
using piHome.Models;

namespace piHome.Events
{
    public class EventBroadcaster : IEventBroadcaster
    {
        public void BroadcastCircuitStateChange(CircuitStateChange change)
        {
            var clients = GlobalHost.ConnectionManager.GetHubContext<PiHomeHub>().Clients;

            clients.All.CircuitStateChanged(change);
        }
    }
}
