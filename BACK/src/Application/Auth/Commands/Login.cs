using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Auth.Commands
{
    public class Login
    {
        public class Command : IRequest<Dtos.Login>
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Username).NotEmpty().MaximumLength(50);
                RuleFor(x => x.Password).NotEmpty().MaximumLength(50);
            }
        }

        public class Handler : IRequestHandler<Command, Dtos.Login>
        {
            private readonly IJwtGenerator jwtGenerator;
            private readonly IConfiguration configuration;

            public Handler(IJwtGenerator jwtGenerator, IConfiguration configuration)
            {
                this.jwtGenerator = jwtGenerator;
                this.configuration = configuration;
            }

            public async Task<Dtos.Login> Handle(Command request, CancellationToken cancellationToken)
            {
                var username = configuration.GetSection("Login").Value;
                var passwd = configuration.GetSection("Password").Value;

                if (username != request.Username || passwd != request.Password)
                    throw new RestException(HttpStatusCode.Unauthorized);

                return new Dtos.Login
                {
                    Token = jwtGenerator.CreateToken(username)
                };
            }
        }
    }
}
