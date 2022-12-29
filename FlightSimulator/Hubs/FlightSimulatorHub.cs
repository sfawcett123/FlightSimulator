using Microsoft.AspNetCore.SignalR;

namespace Listener.Hubs
{
    /// <summary>
    /// SignalR HUB for Flight Simualtor Listener ASP
    /// </summary>
    public class FlightSimulatorHub : Hub
    {
        /// <summary>
        /// Send Data to Flight Simulator Hub
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(string message)
        {
            Clients.All.SendAsync("FlightSimulator", message);
        }
    }
}
