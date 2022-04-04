using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Cards.Queries
{
    public class Details
    {
        public class Query : IRequest<Dtos.CardDto>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Dtos.CardDto>
        {
            private readonly IDataContext context;
            private readonly IMapper mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Dtos.CardDto> Handle(Query request, CancellationToken cancellationToken)
            {
                // handler logic goes here
                var card = await context.Cards.FindAsync(request.Id);
                if (card == null)
                    throw new RestException(HttpStatusCode.NotFound, new { card = "Card not found!" });
                var result = mapper.Map<Card, Dtos.CardDto>(card);
                return result;
            }
        }
    }
}
