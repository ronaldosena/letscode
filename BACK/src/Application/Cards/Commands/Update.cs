using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.Cards.Commands
{
    public class Update
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
            public string Group { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotNull();
            }
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

                card.Title = request.Title ?? card.Title;
                card.Body = request.Body ?? card.Body;
                card.Group = request.Group ?? card.Group;

                var success = await context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;
                throw new RestException(HttpStatusCode.InternalServerError, new { card = "Fail to save changes" });
            }
        }
    }
}
