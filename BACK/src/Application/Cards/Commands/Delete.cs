using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Cards.Commands
{
    public class Delete
    {
        public class Command : IRequest<IEnumerable<Dtos.CardDto>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, IEnumerable<Dtos.CardDto>>
        {
            private readonly IDataContext context;
            private readonly IMapper mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<IEnumerable<Dtos.CardDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var card = await context.Cards.FindAsync(request.Id);

                if (card == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { card = "Card not found!" });

                context.Cards.Remove(card);

                var success = await context.SaveChangesAsync(cancellationToken) > 0;


                if (success)
                {
                    var cards = await context.Cards.Where(_ => true).ToListAsync();
                    var result = mapper.Map<List<Card>, List<Dtos.CardDto>>(cards);
                    return result;
                }
                throw new RestException(HttpStatusCode.InternalServerError, new { card = "Fail to save changes" });
            }
        }
    }
}
