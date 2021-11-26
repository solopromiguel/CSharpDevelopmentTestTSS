using Application.Person.Queries.GetPersonAll;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Person.Queries.GetPersonById
{
   public class GetPersonByIdQuery : IRequest<PersonDto>
    {
        public int Id { get; set; }

        public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, PersonDto>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            public GetPersonByIdQueryHandler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<PersonDto> Handle(GetPersonByIdQuery query, CancellationToken cancellationToken)
            {
                var person = await _context.Persons.Where(a => a.IdPersona == query.Id).FirstOrDefaultAsync();
                if (person == null) return null;

                var result = _mapper.Map<PersonDto>(person);
                return result;
            }
        }
    }
}
