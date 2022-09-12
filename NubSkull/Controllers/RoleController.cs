using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NubSkull.Implementations.Commands;
using NubSkull.Implementations.Queries;

namespace NubSkull.Controllers
{
    [ApiController]
     [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }
 
        [HttpPost("CreateRoleAsync")]
        [Authorize(Roles ="Administator")]
        public async Task<IActionResult> CreateRoleAsync([FromBody] RegisterRoleCommand roleCommand)
        {
            var response  = await _mediator.Send(roleCommand);
            if(!response.IsSuccessful) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("GetAllRoles")]
        [Authorize(Roles ="Administator")]
        public async Task<IActionResult> GetAllRoles()
        {
          var response = await _mediator.Send(new GetAllRolesQuery());
          if(!response.IsSuccessful) return BadRequest(response);
          return Ok(response);
        }

        [HttpGet("GetSelectedRolesQuery")]
        public async Task<IActionResult> GetSelectedRolesQuery([FromQuery]List<string> roleNames)
        {
            var response = await _mediator.Send(new GetSelectedRolesQuery(roleNames));
            if(!response.IsSuccessful) return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("GetRoleByName")]
        public async Task<IActionResult> GetRoleByName([FromQuery] string RoleName)
        {
           var response = await _mediator.Send(new GetRoleByNameQuery(RoleName));
           if(!response.IsSuccessful) return BadRequest(response);
           return Ok(response);
        }
        [HttpPut("UpdateRoleCommand")]
        public async Task<IActionResult> UpdateRoleCommand([FromBody]UpdateRoleCommand updateRoleCommand)
        {
            var response = await _mediator.Send(updateRoleCommand);
            if(!response.IsSuccessful) return BadRequest(response);
            return Ok(response);
        }
    }
}