using Application.Common.Models;
using Application.Person.Queries.GetPersonAll;
using Application.Person.Queries.GetPersonById;
using Application.Person.Queries.GetPersonsWithPagination;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonsController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<PersonDto>>> GetPersonsWithPagination([FromQuery] GetPersonsWithPaginationQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<PersonDto>>> GetAll([FromQuery] GetPersonAllQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _mediator.Send(new GetPersonByIdQuery { Id = id });
            if (entity == null) return NotFound();
            return Ok(entity);
        }
    }
}
