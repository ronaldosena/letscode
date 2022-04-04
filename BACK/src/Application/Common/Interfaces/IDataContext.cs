using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IDataContext
    {
        DbSet<Card> Cards { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
