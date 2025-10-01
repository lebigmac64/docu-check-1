using DocuCheck.Domain.Entities.ChecksHistory;
using DocuCheck.Domain.Entities.ChecksHistory.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocuCheck.Infrastructure.Persistence.Configurations;

public class ChecksHistoryConfigurations : IEntityTypeConfiguration<CheckHistory>
{
    public void Configure(EntityTypeBuilder<CheckHistory> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).ValueGeneratedNever();
            
        entity.Property(e => e.Number)
            .HasConversion(
                number => number.Value,
                value => DocumentNumber.Create(value))
            .HasMaxLength(15)
            .IsRequired();
            
        entity.Property(e => e.CheckedAt)
            .IsRequired();
    }
}