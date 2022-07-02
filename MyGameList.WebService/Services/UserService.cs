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
    [Route("user_api")]
    public class UserService : BaseService
    {

        public UserService(ILogger<BaseService> logger) : base(logger)
        {
        }

        [HttpPost]
        [Route("login")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status200OK)]
        public IActionResult Login([FromBody] UserRequestDTO req)
        {
            try
            {
                var objJSON = UserManager.LoginUser(req);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MessageResponseDTO(ex));
            }
        }

        [HttpPost]
        [Route("register")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status200OK)]
        public IActionResult Register([FromBody] UserRequestDTO req)
        {
            try
            {
                var objJSON = UserManager.RegisterUser(req);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MessageResponseDTO(ex));
            }
        }
    }
}