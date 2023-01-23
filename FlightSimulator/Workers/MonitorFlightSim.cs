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
        private readonly BoardFactory boardFactory;
        private readonly IHubContext<FlightSimulatorHub> hubContext;
        private readonly ILogger<SimulatorFactory> logger;
        private BoardManager.Board? InternalBoard;

        private Timer? timer = null;
        /// <summary>Gets the interval.</summary>
        /// <value>The interval.</value>
        public double Interval { get; private set; } = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimulatorFactory"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="hubContext">The hub context.</param>
        /// <param name="boardFactory">The board controller.</param>
        public SimulatorFactory( ILogger<SimulatorFactory> logger , IHubContext<FlightSimulatorHub> hubContext, BoardFactory boardFactory)
        {
            this.hubContext = hubContext;
            this.logger = logger;
            this.boardFactory = boardFactory;
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
                ConnectToSim();
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

                    logger.LogInformation( $"Internal Board added {boardFactory.Add(InternalBoard)}"  );
                    AddRequests(InternalBoard.Outputs);
                }
                
                // hubContext.Clients.All.SendAsync("FlightSimulatorTrack", TrackData().Serialize());
                boardFactory.SetOutputData(AircraftData());

            }

            hubContext.Clients.All.SendAsync("SimData", AircraftData().Serialize());
        }
    }
}