using BooksAPI.BE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksAPI.BE.Configuration;

public class LibraryComicConfiguration:IEntityTypeConfiguration<LibraryComic>
{
    public void Configure(EntityTypeBuilder<LibraryComic> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Title).IsRequired();
        
        builder.Property(x => x.Author).IsRequired();
       
        builder.Property(x => x.DemographicType).IsRequired();
        
        builder.Property(x => x.ComicType).IsRequired();
        
        builder.Property(x => x.TotalVolumes).IsRequired();
        
        builder.Property(x => x.TotalChapters).IsRequired();

        
    }
}