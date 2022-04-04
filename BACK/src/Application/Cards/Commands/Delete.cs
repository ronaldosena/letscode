using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Cards.Commands
{
    public class Delete
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IDataContext context;

            public Handler(IDataContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var card = await context.Cards.FindAsync(request.Id);

                if (card == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { card = "Card not found!" });

                context.Cards.Remove(card);

                var success = await context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;
                throw new RestException(HttpStatusCode.InternalServerError, new { card = "Fail to save changes" });
            }
        }
    }
}
