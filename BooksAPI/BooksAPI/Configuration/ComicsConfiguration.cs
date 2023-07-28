using BooksAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksAPI.Configuration;

public class ComicsConfiguration : BookConfiguration<Comic>
{
    public override void Configure(EntityTypeBuilder<Comic> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.DemographicType).IsRequired();

        builder.Property(x => x.ComicType).IsRequired();

        builder.Property(x => x.PublishingStatus).IsRequired();

        builder.Property(x => x.TotalVolumes).IsRequired();

        builder.Property(x => x.CollectedVolumes).IsRequired();
    }
}