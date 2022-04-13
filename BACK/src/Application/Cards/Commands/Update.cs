using System.Net;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Application.Cards.Commands
{
    public class Update
    {
        public class Command : IRequest<Dtos.CardDto>
        {
            public string Id { get; set; }
            [JsonPropertyName("titulo")]
            public string Title { get; set; }
            [JsonPropertyName("conteudo")]
            public string Body { get; set; }
            [JsonPropertyName("lista")]
            public string Group { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, Dtos.CardDto>
        {
            private readonly IDataContext context;
            private readonly IMapper mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Dtos.CardDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var card = await context.Cards.FindAsync(request.Id);

                if (card == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { card = "Card not found!" });

                card.Title = request.Title ?? card.Title;
                card.Body = request.Body ?? card.Body;
                card.Group = request.Group ?? card.Group;

                var success = await context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return mapper.Map<Dtos.CardDto>(card);
                throw new RestException(HttpStatusCode.InternalServerError, new { card = "Fail to save changes" });
            }
        }
    }
}
