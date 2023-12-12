using System.Reflection;
using Exchanger.Application.Common.Interfaces;
using Exchanger.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exchanger.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Exchange> Exchanges => Set<Exchange>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
