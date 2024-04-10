using BooksAPI.BE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksAPI.BE.Configuration;

public class UserMangaConfiguration:IEntityTypeConfiguration<UserManga>
{
    public void Configure(EntityTypeBuilder<UserManga> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.ReadingStatus).IsRequired();
        
        builder.Property(x => x.ReadVolumes).IsRequired();
        
        builder.Property(x => x.CollectedVolumes).IsRequired();
        
        builder.Property(x => x.Price).IsRequired();
        
        builder.Property(x => x.CollectionStatus).IsRequired();

        

    }
}