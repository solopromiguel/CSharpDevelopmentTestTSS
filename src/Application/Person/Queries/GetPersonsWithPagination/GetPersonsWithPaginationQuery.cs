using Application.Common.Mappings;
using Application.Common.Models;
using Application.Person.Queries.GetPersonAll;
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

namespace Application.Person.Queries.GetPersonsWithPagination
{
   public class GetPersonsWithPaginationQuery : IRequest<PaginatedList<PersonDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
    public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetPersonsWithPaginationQuery, PaginatedList<PersonDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTodoItemsWithPaginationQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<PersonDto>> Handle(GetPersonsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Persons
                .AsNoTracking()
                .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
