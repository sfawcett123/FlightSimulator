using BoardManager;
using Listener.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Net.Sockets;

namespace Listener.Workers
{
    /// <summary>
    ///   <br />
    /// </summary>
    public class BoardFactory : BoardList, IHostedService, IDisposable
    {
        private readonly Timer timer;
        private readonly IHubContext<FlightSimulatorHub> hubContext;
        /// <exclude />
        public double Interval { get; set; } = 1;
        /// <exclude />
        public BoardFactory(IHubContext<FlightSimulatorHub> hubContext)
        {
            this.hubContext = hubContext;
            timer = new Timer(Process, null, TimeSpan.Zero, TimeSpan.FromSeconds(Interval));
        }
        /// <exclude />
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        /// <exclude />
        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        void IDisposable.Dispose()
        {
            timer.Dispose();
            GC.SuppressFinalize(this);
        }
        private void Process(object? state)
        {
            //TODO: Remove Timed out boards function not working.
            // RemoveTimedOut();

            // hubContext.Clients.All.SendAsync("Boards", Serialize());

        }
    }
}