using BoardManager;
using Listener.Helpers;
using Listener.Hubs;
using Microsoft.AspNetCore.SignalR;
using SimListener;

namespace Listener.Workers
{
    /// <summary>
    ///   <br />
    /// </summary>
    public class SimulatorFactory : Connect, IHostedService, IDisposable
    {
        private readonly BoardFactory boardController;
        private readonly IHubContext<FlightSimulatorHub> hubContext;
        private BoardManager.Board? InternalBoard;

        private Timer? timer = null;
        /// <summary>Gets the interval.</summary>
        /// <value>The interval.</value>
        public double Interval { get; private set; } = 1;

        /// <summary>Initializes a new instance of the <see cref="SimulatorFactory" /> class.</summary>
        /// <param name="hubContext">The hub context.</param>
        /// <param name="boardController">The board controller.</param>
        public SimulatorFactory( IHubContext<FlightSimulatorHub> hubContext, BoardFactory boardController)
        {
            this.hubContext = hubContext;
            this.boardController = boardController;
        }

        /// <summary>Starts the asynchronous.</summary>
        /// <param name="stoppingToken">The stopping token.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task StartAsync(CancellationToken stoppingToken)
        {
            timer = new Timer(Process, null, TimeSpan.Zero, TimeSpan.FromSeconds(Interval));
            return Task.CompletedTask;
        }

        /// <summary>Stops the asynchronous.</summary>
        /// <param name="stoppingToken">The stopping token.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task StopAsync(CancellationToken stoppingToken)
        {
            Disconnect();
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        void IDisposable.Dispose()
        {
            Disconnect();
            timer?.Dispose();
            GC.SuppressFinalize(this);
        }

        private void Process(object? state)
        {

            if (Connected == false)
            {
                ConnecttoSim();
                InternalBoard= null;
            }

            if (Connected == true)
            {
                if (InternalBoard is null)
                {
                    InternalBoard = new()
                    {
                        Name = "Internal",
                        OperatingSystem = "Windows",
                        Outputs = new List<string>
                                          {   "ATC MODEL",
                                              "ATC TYPE",
                                              "PLANE LATITUDE",
                                              "PLANE LONGITUDE",
                                              "PLANE HEADING DEGREES TRUE" },
                    };

                    Console.WriteLine( $"Internal Board add {0}" , boardController.Add(InternalBoard));
                    AddRequests(InternalBoard.Outputs);
                }
  
                hubContext.Clients.All.SendAsync("FlightSimulator", AircraftData().Serialize());
                hubContext.Clients.All.SendAsync("FlightSimulatorTrack", TrackData().Serialize());
                boardController.SetOutputData(AircraftData());

            }
        }
    }
}