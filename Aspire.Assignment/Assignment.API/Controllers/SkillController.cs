using Assignment.Contracts.DTO;
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
    [Authorize]
    public class SkillController : Controller
    {
        private readonly IMediator _mediator;
        public SkillController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("{categoryId}")]
        [ProducesResponseType(typeof(IEnumerable<SkillDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Get(string categoryId)
        {
            var query = new GetSpecifiedSkillsQuery(Guid.Parse(categoryId));
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
