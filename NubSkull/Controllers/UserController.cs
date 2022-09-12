using MediatR;
using Microsoft.AspNetCore.Mvc;
using NubSkull.Authentication;
using NubSkull.DTOs;
using NubSkull.Implementations.Commands;
using NubSkull.Implementations.Queries;

namespace NubSkull.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediatr;
        private readonly IJWTTokenHandler _jwtTokenHandler;

        public UserController(IMediator mediatr, IJWTTokenHandler jwtTokenHandler)
        {
            _mediatr = mediatr;
            _jwtTokenHandler = jwtTokenHandler;
        }

        [HttpPost("CreateUserAsync")]
        public async Task<IActionResult> CreateUserAsync([FromBody] RegisterUserCommand registerUserCommand)
        {
            var response = await _mediatr.Send(registerUserCommand);
            if (!response.IsSuccessful) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("GetAllUsersAsync")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var response = await _mediatr.Send(new GetAllUsersQuery());
            if (!response.IsSuccessful) return BadRequest(response);
            return Ok(response);
        }
        [HttpPut("UpdateUserAsync")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserCommand updateUserCommand)
        {
            var response = await _mediatr.Send(updateUserCommand);
            if (!response.IsSuccessful) return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            var response = await _mediatr.Send(new GetUserByEmailQuery(email));
            if(!response.IsSuccessful) return BadRequest(response);
            return Ok(response);
        }
        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin([FromBody] LoginCommand loginCommand)
        {
            var response = await _mediatr.Send(loginCommand);
            var loginResponse = new LoginResponseModel
            {
               Token = _jwtTokenHandler.GenerateToken(response.Data),
               UserLoggedIn =  new UserDto
               {
                  EmailAddress = response.Data.EmailAddress,
                  Id = response.Data.Id,
                  UserName = response.Data.UserName,
                  UserRoles = response.Data.UserRoles
               }
            };
            if(!response.IsSuccessful) return BadRequest(response);
            return Ok(loginResponse);

        }
    }
}