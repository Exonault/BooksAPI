using BooksAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksAPI.Configuration;

public class BookConfiguration<T> : IEntityTypeConfiguration<T> where T : Book
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Author).IsRequired();

        builder.Property(x => x.Title).IsRequired();

        builder.Property(x => x.Price).IsRequired();

        builder.Property(x => x.ReadingStatus).IsRequired();
    }
}