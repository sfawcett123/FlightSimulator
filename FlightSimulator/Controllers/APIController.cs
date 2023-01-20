using Listener.Workers;
using Microsoft.AspNetCore.Mvc;


namespace Listener.Controllers
{
    /// <summary>
    ///   <br />
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class APIController : Controller
    {
        private readonly Workers.SimulatorFactory FlightSimulator;
        private readonly Workers.BoardFactory BoardController;
        private readonly IConfiguration configuration;
        private readonly ILogger<APIController> logger;

        private string? GetDocumentBase()
        {
            return this.configuration.GetSection("Documents").GetSection("Location").Value  ;
        }
        /// <summary>Initializes a new instance of the <see cref="APIController" /> class.</summary>
        /// <param name="logger">The Logger.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="hostedService">The hosted service.</param>
        /// <param name="boardController">The board controller.</param>
        public APIController(ILogger<APIController> logger, IConfiguration configuration , Workers.SimulatorFactory hostedService, Workers.BoardFactory boardController)
        {
            FlightSimulator = hostedService;
            BoardController = boardController;
            this.configuration = configuration;
            this.logger = logger;
        }

        // POST api/<APIController>
        // TODO: Add some defaults which will make Swagger work
        /// <summary>Registers a board with the Sim Listener</summary>
        /// <param name="_name">The details of the board.</param>
        /// <response code="201">Board Accepted</response>
        /// <response code="400">Invalid Data in request</response>
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] BoardManager.Board _name)
        {
            logger.LogInformation($"Registering {_name.Name}");
            var output = "OK";

            if ( _name.Outputs is not null)
                output = FlightSimulator.AddRequests(_name.Outputs);

            if( output != "OK"  )    
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest,
                                        type: $"{GetDocumentBase()}/errors/request.html",
                                    detail: $"The requested output ({output}) cannot be found",
                                    title: "Invalid Output Request",
                                    instance: HttpContext.Request.Path);
            }

            try
            {
                string rval = BoardController.Add(_name, Request.HttpContext.Connection);
                logger.LogInformation($"External Board added {rval}");

                return new ObjectResult(rval)
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest,
                                     type: $"{GetDocumentBase()}/errors/boards.html",
                                   detail: $"The board ({_name.Name}) cannot be added",
                                    title: "Invalid Board Request",
                                 instance: HttpContext.Request.Path);
            }
        }

    }
}

