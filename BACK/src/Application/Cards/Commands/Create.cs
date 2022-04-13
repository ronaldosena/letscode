using System;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Cards.Commands
{
    public class Create
    {
        public class Command : IRequest<Dtos.CardDto>
        {
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
                RuleFor(x => x.Title).MaximumLength(50);
                RuleFor(x => x.Body).MaximumLength(1000);
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

                var card = new Card
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = request.Title,
                    Body = request.Body,
                    Group = request.Group
                };

                context.Cards.Add(card);
                var success = await context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return mapper.Map<Dtos.CardDto>(card);
                throw new RestException(HttpStatusCode.InternalServerError, new { card = "Fail to save changes" });
            }
        }
    }
}
