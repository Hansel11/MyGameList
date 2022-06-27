using System;
using System.Reflection;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using MyGameList.WebService.Application;
using MyGameList.Domain.Response;
using MyGameList.Domain.Request;
using psdtest.Domain.Response;

namespace MyGameList.WebService.Services
{
    [ApiController]
    [Route("game_api")]
    public class GameService : BaseService
    {

        public GameService(ILogger<BaseService> logger) : base(logger)
        {
        }

        [HttpPost]
        [Route("get")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GameResponseDTO), StatusCodes.Status200OK)]
        public IActionResult GetGame([FromBody] GameRequestDTO req)
        {
            try
            {
                var objJSON = GameManager.GetGame(req.Id);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MessageResponseDTO(ex));
            }
        }

        [HttpPost]
        [Route("get_all")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<GameResponseDTO>), StatusCodes.Status200OK)]
        public IActionResult GetAllGame([FromBody] GameRequestDTO req)
        {
            try
            {
                var objJSON = GameManager.GetAllGame(req.UserId);
                return new OkObjectResult(objJSON);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new MessageResponseDTO(ex));
            }
        }

        [HttpPost]
        [Route("add")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MessageResponseDTO), StatusCodes.Status200OK)]
        public IActionResult AddGame([FromBody] GameRequestDTO req)
        {
            try
            {
                GameManager.AddGame(req);
                var objJSON = new MessageResponseDTO();
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MessageResponseDTO(ex));
            }
        }

        [HttpPost]
        [Route("edit")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MessageResponseDTO), StatusCodes.Status200OK)]
        public IActionResult EditGame([FromBody] GameRequestDTO req)
        {
            try
            {
                GameManager.EditGame(req);
                var objJSON = new MessageResponseDTO();
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MessageResponseDTO(ex));
            }
        }

        [HttpPost]
        [Route("remove")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MessageResponseDTO), StatusCodes.Status200OK)]
        public IActionResult RemoveGame([FromBody] GameRequestDTO req)
        {
            try
            {
                GameManager.RemoveGame(req.Id);
                var objJSON = new MessageResponseDTO();
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MessageResponseDTO(ex));
            }
        }
    }
}