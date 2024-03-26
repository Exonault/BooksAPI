using AutoMapper;
using BooksAPI.BE.Contracts.UserComic;
using BooksAPI.BE.Data;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Mapping;
using BooksAPI.BE.Repositories;
using BooksAPI.BE.Validation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace BooksAPI.Test;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void TestMappingRequest()
    {
        MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
        {
            config.AddProfile(new UserComicProfile());
        });

        IMapper mapper = mapperConfiguration.CreateMapper();

        CreateUserComicRequest request = new CreateUserComicRequest
        {
            ReadingStatus = "Reading",
            ReadVolumes = 12,
            ReadChapters = 111,
            CollectedVolumes = 12,
            Price = 0,
            CollectionStatus = "Collecting",
            UserId = "2f9f94b0-2b87-4892-b95e-5377df0cc42b",
            LibraryComicId = Guid.Parse("323c9333-c943-41c8-932e-3dc295eda791")
        };

        UserComic userComic = mapper.Map<UserComic>(request);
        //Doesnt map user and libraryComic (manual mapping needs to be done)

        Console.WriteLine();
    }

    [Fact]
    public async void TestMappingResponse()
    {
        MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
        {
            config.AddProfile(new UserComicProfile());
        });

        IMapper mapper = mapperConfiguration.CreateMapper();

        DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder()
            .UseNpgsql("Server=localhost;Database=testDbBooks;Username=postgres;Password=1234");

        UserComic userComic;
        await using (var db = new ApplicationDbContext(dbContextOptionsBuilder.Options))
        {
            User user = db.Users.FirstOrDefault(x => true)!;
            LibraryComic libraryComic = db.LibraryComics.FirstOrDefault(x => true)!;

            userComic = new UserComic
            {
                Id = Guid.NewGuid(),
                ReadingStatus = "Reading",
                ReadVolumes = 11,
                ReadChapters = 21,
                CollectedVolumes = 10,
                Price = 18,
                CollectionStatus = "Collecting",
                User = user,
                LibraryComic = libraryComic
            };
        }

        UserComicResponse userComicResponse = mapper.Map<UserComicResponse>(userComic);
        //Maps UserId but not the LibraryComicResponse
        Console.WriteLine();
    }

    [Fact]
    public async void TestValidation()
    {
        UserComicValidator userComicValidator = new UserComicValidator();

        DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder()
            .UseNpgsql("Server=localhost;Database=testDbBooks;Username=postgres;Password=1234");
        UserComic userComic;

        await using (var db = new ApplicationDbContext(dbContextOptionsBuilder.Options))
        {
            User user = db.Users.FirstOrDefault(x => true)!;
            //LibraryComic libraryComic = db.LibraryComics.FirstOrDefault(x => true)!;

            LibraryComic libraryComic = new LibraryComic()
            {
                Id = Guid.NewGuid(),
                Title = "null",
                Author = "null",
                DemographicType = "null",
                ComicType = "null",
                PublishingStatus = "null",
                TotalVolumes = 0,
                TotalChapters = 0,
            };


            userComic = new UserComic
            {
                Id = Guid.NewGuid(),
                ReadingStatus = "Reading",
                ReadVolumes = 11,
                ReadChapters = 21,
                CollectedVolumes = 10,
                Price = 18,
                CollectionStatus = "Collecting",
                User = user,
                LibraryComic = libraryComic
            };
        }

        ValidationResult validationResult = await userComicValidator.ValidateAsync(userComic);

        Console.WriteLine(validationResult.Errors);
    }

    [Fact]
    public async void TestDb()
    {
        DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder()
            .UseNpgsql("Server=localhost;Database=testDbBooks;Username=postgres;Password=1234");

        await using (var db = new ApplicationDbContext(dbContextOptionsBuilder.Options))
        {
            UserComic? firstOrDefaultAsync = await db.UserComics
                .Include(x => x.User)
                .Include(x => x.LibraryComic)
                .FirstOrDefaultAsync();

            Console.WriteLine();
        }

    }

    [Fact]
    public async void TestMapping()
    {
        MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
        {
            config.AddProfile(new UserComicProfile());
        });

        IMapper mapper = mapperConfiguration.CreateMapper();


        DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder()
            .UseNpgsql("Server=localhost;Database=testDbBooks;Username=postgres;Password=1234");

        UserComic userComic;
        UpdateUserComicRequest updateUserComicRequest = new UpdateUserComicRequest
        {
            ReadingStatus = "Reading",
            ReadVolumes = 1211,
            ReadChapters = 11111,
            CollectedVolumes = 111,
            Price = 112.11m,
            CollectionStatus = "Finished",
            UserId = "",
            LibraryComicId = Guid.NewGuid()
        };

        await using (var db = new ApplicationDbContext(dbContextOptionsBuilder.Options))
        {
            UserComic? uc = await db.UserComics
                .Include(x => x.User)
                .Include(x => x.LibraryComic)
                .FirstOrDefaultAsync();

            mapper.Map(updateUserComicRequest, uc);

            Console.WriteLine();
        }


    }

    [Fact]
    public async void TestValidationOrder()
    {
        OrderValidator orderValidator = new OrderValidator();

        DateTime dateTime = new DateTime(2024, 3, 7);

        Order order = new Order
        {
            Id = Guid.NewGuid(),
            Date = DateOnly.FromDateTime(dateTime),
            Description = "something",
            Place = "somewhere",
            Amount = 12.33m,
            NumberOfItems = 1,
            User = null
        };

        ValidationResult validationResult = await orderValidator.ValidateAsync(order);

        Console.WriteLine();

    }
    
}