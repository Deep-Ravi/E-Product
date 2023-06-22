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
    [Authorize(Roles = "ADMIN")]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [TypeFilter(typeof(OperationFilters), Arguments = new Object[] {nameof(OperationAccessEnum.VIEW)})]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllProductQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(System.Guid), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [TypeFilter(typeof(OperationFilters), Arguments = new Object[] {nameof(OperationAccessEnum.ADD)})]
        public async Task<IActionResult> Post([FromBody] CreateProductDTO model)
        {
            try
            {
                var command = new CreateProductCommand(model);
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
        [TypeFilter(typeof(OperationFilters), Arguments = new Object[] {nameof(OperationAccessEnum.EDIT)})]
        public async Task<IActionResult> Put([FromBody] ProductDTO model)
        {
            try
            {
                var command = new UpdateProductCommand(model);
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
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [TypeFilter(typeof(OperationFilters), Arguments = new Object[] {nameof(OperationAccessEnum.DELETE) })]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var command = new DeleteProductCommand(Guid.Parse(id));
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
