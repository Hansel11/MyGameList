using System;
using System.Reflection;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using MyGameList.WebService.Application;
using MyGameList.WebService.Output;
using MyGameList.Domain.Response;

namespace MyGameList.WebService.Services
{
    [ApiController]
    [Route("game_api")]
    public class GameService : BaseService
    {

        public GameService(ILogger<BaseService> logger) : base(logger)
        {
        }

        /// <summary>
        /// Get All Test List
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /test
        ///
        /// </remarks>
        /// <returns>A list of Test</returns>
        /// <response code="200">Returns a list of Test</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GameResponseDTO), StatusCodes.Status200OK)]
        public IActionResult GetGame()
        {
            try
            {
                
                var objJSON = GameManager.GetGame(Guid.Parse("3E815376-FC29-44AA-9B8B-580B179BAB30"));
                return new OkObjectResult(objJSON);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }
    }
}