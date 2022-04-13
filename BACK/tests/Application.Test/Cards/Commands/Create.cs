using System.Threading;
using System.Threading.Tasks;
using Application.Test.Common;
using Xunit;
using FluentAssertions;

namespace Application.Test.Cards
{
    public class Create : BaseCommand
    {
        [Fact]
        public async Task should_persist_card()
        {
            var command = new Application.Cards.Commands.Create.Command
            {
                Title = "Fake Title",
                Body = "Fake Body",
                Group = "To Do"
            };

            var handler = new Application.Cards.Commands.Create.Handler(Context, null);

            var result = await handler.Handle(command, CancellationToken.None);

            var entity = Context.Cards.Find(result);

            entity.Should().NotBeNull();
            entity.Title.Should().Be(command.Title);
            entity.Body.Should().Be(command.Body);
            entity.Group.Should().Be(command.Group);
        }
    }
}
