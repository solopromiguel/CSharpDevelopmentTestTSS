using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Person.Queries.GetPersonAll
{
   public class GetPersonAllQuery : IRequest<List<PersonDto>>
    {
        public string Nombres { get; set; }
        public string Apellido { get; set; }
        
    }

    public class GetPersonAllQueryHandler : IRequestHandler<GetPersonAllQuery, List<PersonDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPersonAllQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PersonDto>> Handle(GetPersonAllQuery request, CancellationToken cancellationToken)
        {
            var records = await _context.Persons
                    .AsNoTracking()
                    .Where(x => string.IsNullOrWhiteSpace(request.Nombres) || x.Nombres.Contains(request.Nombres))
                    .Where(x=> string.IsNullOrWhiteSpace(request.Apellido) || x.Apellidos.Contains(request.Apellido))                    
                    .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

            return records;
        }
    }
}
