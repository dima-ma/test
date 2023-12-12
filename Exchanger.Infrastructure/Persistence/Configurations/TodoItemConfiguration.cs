using Exchanger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exchanger.Infrastructure.Persistence.Configurations;

public class TodoItemConfiguration : IEntityTypeConfiguration<Exchange>
{
    public void Configure(EntityTypeBuilder<Exchange> builder)
    {
        builder.Property(t => t.Currency)
            .HasMaxLength(3)
            .IsRequired();
        
        builder.Property(t => t.Result)
            .IsRequired();
    }
}
