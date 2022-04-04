using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Cards.Commands
{
    public class Create
    {
        public class Command : IRequest<int>
        {
            public string Title { get; set; }
            public string Body { get; set; }
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

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly IDataContext context;

            public Handler(IDataContext context)
            {
                this.context = context;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {

                var card = new Card
                {
                    Title = request.Title,
                    Body = request.Body,
                    Group = request.Group
                };

                context.Cards.Add(card);
                var success = await context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return card.Id;
                throw new RestException(HttpStatusCode.InternalServerError, new { card = "Fail to save changes" });
            }
        }
    }
}
