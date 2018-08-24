using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lively.Football.Application.Countries.Entities
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).IsRequired();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(256);
            builder.Property(c => c.SourceId).IsRequired().HasMaxLength(256);

            builder.HasIndex(c => c.SourceId).IsUnique();
        }
    }
}
