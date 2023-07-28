using BooksAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksAPI.Configuration;

public class ComicsConfiguration : BookConfiguration<Comic>
{
    public override void Configure(EntityTypeBuilder<Comic> builder)
    {
        base.Configure(builder);

        // builder.HasKey(x => x.Id);
        // builder.Property(x => x.Id).ValueGeneratedOnAdd();
        //
        // builder.Property(x => x.Author).IsRequired().HasMaxLength(50);
        //
        // builder.Property(x => x.Title).IsRequired().HasMaxLength(60);
        //
        // builder.Property(x => x.Price).IsRequired();
        //
        // builder.Property(x => x.ReadingStatus).IsRequired();

        builder.Property(x => x.DemographicType).IsRequired();

        builder.Property(x => x.ComicType).IsRequired();

        builder.Property(x => x.PublishingStatus).IsRequired();

        builder.Property(x => x.TotalVolumes).IsRequired();

        builder.Property(x => x.CollectedVolumes).IsRequired();
    }
}