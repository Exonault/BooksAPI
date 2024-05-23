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

        builder.Property(x => x.TitleRomaji).IsRequired();
        
        builder.Property(x => x.TitleEnglish).IsRequired(false);
        
        builder.Property(x => x.TitleJapanese).IsRequired();
       
        builder.Property(x => x.DemographicType).IsRequired();
        
        builder.Property(x => x.Type).IsRequired();
        
        builder.Property(x=> x.PublishingStatus).IsRequired();

        builder.Property(x => x.TotalVolumes).IsRequired(false);

        builder.Property(x => x.MainImageUrl).IsRequired(false);

        builder.Property(x => x.Synopsis).IsRequired();
    }
}