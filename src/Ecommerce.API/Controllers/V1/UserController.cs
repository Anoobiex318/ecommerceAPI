using Ecommerce.Application.Commands;
using Ecommerce.Application.Queries;
using Ecommerce.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddUser([FromBody] AddUserDTO addUserDTO)
        {
            var command = new UserCommand.AddUserCommand
            {
                UserName = addUserDTO.UserName
            };
            var user = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUserById), new { userId = user.UserId }, user);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserById()
        {
            // Extract the x-user-id from the headers
            var userId = Request.Headers["x-user-id"].FirstOrDefault();

            if (string.IsNullOrEmpty(userId))
            {
                // If x-user-id is not provided, return an Unauthorized status
                return Unauthorized();
            }

            var parsedUserId = Guid.Parse(userId);

            var query = new UserQuery.GetUserByIdQuery
            {
                UserId = parsedUserId
            };

            var user = await _mediator.Send(query);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

    }
}
