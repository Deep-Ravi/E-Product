using Assignment.Contracts.DTO;
using Assignment.Contracts.Enum;
using Assignment.Core.Exceptions;
using Assignment.Core.Filters;
using Assignment.Core.Handlers.Commands;
using Assignment.Core.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillSetController : Controller
    {
        private readonly IMediator _mediator;
        public SkillSetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{userName}")]
        [ProducesResponseType(typeof(IEnumerable<SkillSetDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [Authorize(Roles="DEVELOPER")]
        [TypeFilter(typeof(OperationFilters), Arguments = new Object[] { nameof(OperationAccessEnum.VIEW) })]
        public async Task<IActionResult> Get(string userName)
        {
            var query = new GetAllDeveloperSkillSetQuery(userName);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(System.Guid), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [Authorize(Roles = "DEVELOPER")]
        [TypeFilter(typeof(OperationFilters), Arguments = new Object[] { nameof(OperationAccessEnum.ADD) })]
        public async Task<IActionResult> Post([FromBody] CreateSkillSetDTO model)
        {
            try
            {
                var command = new CreateSkillSetCommand(model);
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

        [HttpPut]
        [ProducesResponseType(typeof(System.Guid), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [Authorize(Roles = "DEVELOPER")]
        [TypeFilter(typeof(OperationFilters), Arguments = new Object[] { nameof(OperationAccessEnum.EDIT) })]
        public async Task<IActionResult> Put([FromBody] SkillSetDTO model)
        {
            try
            {
                var command = new UpdateSkillSetCommand(model);
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

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [Authorize(Roles = "DEVELOPER")]
        [TypeFilter(typeof(OperationFilters), Arguments = new Object[] { nameof(OperationAccessEnum.DELETE) })]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var command = new DeleteSkillSetCommand(Guid.Parse(id));
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
        [HttpGet]
        [Route("Approvals")]
        [ProducesResponseType(typeof(IEnumerable<SkillSetDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [Authorize(Roles = "MANAGER")]
        [TypeFilter(typeof(OperationFilters), Arguments = new Object[] { nameof(OperationAccessEnum.VIEW) })]
        public async Task<IActionResult> GetApprovalSkillSets()
        {
            var query = new GetAllApprovalSkillSetQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPut]
        [Route("Approvals")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [Authorize(Roles = "DEVELOPER")]
        [TypeFilter(typeof(OperationFilters), Arguments = new Object[] { nameof(OperationAccessEnum.VIEW) })]
        public async Task<IActionResult> SetApprovalSkillSets(IEnumerable<SkillSetDTO> skillSetDTOs)
        {
            try
            {
                var query = new UpdateAllApprovalCommand(skillSetDTOs);
                var response = await _mediator.Send(query);
                return Ok(response);
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

        [HttpPut]
        [Route("Approval")]
        [ProducesResponseType(typeof(System.Guid), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [Authorize(Roles = "MANAGER")]
        [TypeFilter(typeof(OperationFilters), Arguments = new Object[] { nameof(OperationAccessEnum.EDIT) })]
        public async Task<IActionResult> UpdateApprovalStatus([FromBody] SkillSetDTO model)
        {
            try
            {
                var command = new UpdateManagerApprovalCommand(model);
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

        [HttpGet]
        [Route("Notify")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [Authorize(Roles = "MANAGER")]
        [TypeFilter(typeof(OperationFilters), Arguments = new Object[] { nameof(OperationAccessEnum.VIEW) })]
        public async Task<IActionResult> GetNotifyNewSkillSet()
        {
            var query = new GetNotifyNewSkillSetCountQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
