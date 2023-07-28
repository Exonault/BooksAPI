﻿using BooksAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksAPI.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Date).IsRequired();

        builder.Property(x => x.Description).IsRequired();

        builder.Property(x => x.Place).IsRequired();

        builder.Property(x => x.Amount).IsRequired();

        builder.Property(x => x.NumberOfItems).IsRequired();

        builder.HasMany(x => x.Comics)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.Id);
    }
}