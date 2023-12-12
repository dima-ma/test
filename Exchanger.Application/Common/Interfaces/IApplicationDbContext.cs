using Exchanger.Domain.Entities;

namespace Exchanger.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Exchange> Exchanges { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
