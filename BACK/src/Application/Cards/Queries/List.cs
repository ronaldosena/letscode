using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Pagination;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Application.Cards.Queries
{
    public class List
    {
        public class ListParams : PageParams
        {
            private string order;

            public string Sort { get; set; } = "Id";
            public string Order { get => order; set => order = PaginationSettings.SetOrder(value); }
        }

        public class Query : IRequest<IEnumerable<Dtos.CardDto>>
        {
            public ListParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<Dtos.CardDto>>
        {
            private readonly IDataContext context;
            private readonly IMapper mapper;
            private readonly IResponseAccessor responseHeader;

            public Handler(IDataContext context, IMapper mapper, IResponseAccessor responseHeader)
            {
                this.responseHeader = responseHeader;
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<IEnumerable<Dtos.CardDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                // handler logic goes here
                var queryable = context.Cards.OrderBy($"{request.Params.Sort} {request.Params.Order}").AsQueryable();

                var cards = await queryable
                    .Skip(request.Params.Skip())
                    .Take(request.Params.Limit)
                    .ToListAsync();

                responseHeader.AddPagination(new PaginationHeader(request.Params.Page, request.Params.Limit, queryable.Count()));

                var result = mapper.Map<List<Card>, List<Dtos.CardDto>>(cards);
                return result;
            }
        }
    }
}
