using Assignment.Contracts.DTO;
using Assignment.Contracts.Enum;
using Assignment.Core.Exceptions;
using Assignment.Core.Filters;
using Assignment.Core.Handlers.Commands;
using Assignment.Core.Handlers.Queries;
using Assignment.Providers.Handlers.Commands;
using Assignment.Providers.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public UserController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [TypeFilter(typeof(OperationFilters), Arguments = new Object[] { nameof(OperationAccessEnum.VIEW) + "," + nameof(OperationAccessEnum.ADD) + "," + nameof(OperationAccessEnum.EDIT) + "," + nameof(OperationAccessEnum.DELETE) })]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllUserQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateUserDTO model)
        {
            try
            {
                var command = new CreateUserCommand(model);
                var response = await _mediator.Send(command);
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Authentication:Jwt:Secret"));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("userId", model.Username.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                // Return the user and token in the response
                return Ok(new
                {
                    user = model,
                    token = tokenHandler.WriteToken(token)
                });
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }

        [HttpGet]
        [Route("Roles")]
        [ProducesResponseType(typeof(IEnumerable<RoleDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [TypeFilter(typeof(OperationFilters), Arguments = new Object[] { nameof(OperationAccessEnum.VIEW) })]
        public async Task<IActionResult> GetRoles()
        {
            var query = new GetAllRoleQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        [HttpPut]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [AllowAnonymous]
        public async Task<IActionResult> Put([FromBody] UserDTO model)
        {
            try
            {
                var command = new UpdateUserCommand(model);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [TypeFilter(typeof(OperationFilters), Arguments = new Object[] { nameof(OperationAccessEnum.DELETE) })]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var command = new DeleteUserCommand(id);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.OK, response);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }
    }
}