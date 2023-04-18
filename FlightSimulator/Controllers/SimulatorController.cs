using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace FlightSimulator.Controllers
{
    /// <summary>Class simulatorController.
    /// Implements the <see cref="ControllerBase" /></summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class simulatorController : ControllerBase
    {
        // POST api/<APIController>
        /// <summary>Flight Simulator StartUp</summary>
        [HttpPost]
        [Route("startup")]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public IActionResult StartUp()
        {
            return new ObjectResult("Not Implemented")
            {
                StatusCode = StatusCodes.Status501NotImplemented
            };
        }

        // POST api/<APIController>
        /// <summary>Flight Simulator Shutdown</summary>
        [HttpPost]
        [Route("shutdown")]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public IActionResult ShutDown()
        {
            return new ObjectResult("Not Implemented")
            {
                StatusCode = StatusCodes.Status501NotImplemented
            };
        }
    }
}
