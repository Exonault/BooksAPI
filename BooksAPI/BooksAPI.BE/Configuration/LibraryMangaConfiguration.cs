using BooksAPI.BE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksAPI.BE.Configuration;

public class LibraryMangaConfiguration:IEntityTypeConfiguration<LibraryManga>
{
    public void Configure(EntityTypeBuilder<LibraryManga> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Title).IsRequired();
       
        builder.Property(x => x.DemographicType).IsRequired();
        
        builder.Property(x => x.Type).IsRequired();
        
        builder.Property(x=> x.PublishingStatus).IsRequired();

        builder.Property(x => x.TotalVolumes).IsRequired(false);

    }
}