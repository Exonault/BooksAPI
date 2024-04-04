using BooksAPI.BE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksAPI.BE.Configuration;

public class UserComicConfiguration:IEntityTypeConfiguration<UserComic>
{
    public void Configure(EntityTypeBuilder<UserComic> builder)
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