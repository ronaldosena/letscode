using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Persistence
{
    public static class Seeding
    {
        public static async Task SeedAsync(DataContext context)
        {
            if (!context.Cards.Any())
            {
                var cards = new List<Card>
                {
                    new Card
                    {
                        Title = "Sample Card 1",
                        Body = @"
                            # h1 Heading 8-)
                            ## h2 Heading
                            ### h3 Heading
                            #### h4 Heading
                            ##### h5 Heading
                            ###### h6 Heading
                        ",
                        Group = "To Do"                       
                    },
                };

                await context.Cards.AddRangeAsync(cards);
                await context.SaveChangesAsync();
            }
        }
    }
}
