﻿using Application.Common.Models;
using Application.Person.Queries.GetPersonAll;
using Application.Person.Queries.GetPersonsWithPagination;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Api.Controllers
{
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
    }
}
