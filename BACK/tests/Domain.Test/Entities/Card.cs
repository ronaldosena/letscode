using Xunit;
using FluentAssertions;

namespace Domain.Test.Entities
{
    public class Card
    {
        [Fact]
        public void should_create_card_with_empty_props()
        {
            var card = new Domain.Entities.Card();
            card.Should().NotBeNull();
            card.Title.Should().BeEmpty();
            card.Body.Should().BeEmpty();
        }
    }
}
