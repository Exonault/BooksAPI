using AutoMapper;
using BooksAPI.BE.Contracts.Author;
using BooksAPI.BE.Contracts.LibraryComic;
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
            config.AddProfile(new LibraryComicProfile());
        });

        IMapper mapper = mapperConfiguration.CreateMapper();

        CreateUserComicRequest request = new CreateUserComicRequest
        {
            ReadingStatus = "Reading",
            ReadVolumes = 12,
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
                //Author = "null",
                DemographicType = "null",
                ComicType = "null",
                PublishingStatus = "null",
                TotalVolumes = 0,
            };


            userComic = new UserComic
            {
                Id = Guid.NewGuid(),
                ReadingStatus = "Reading",
                ReadVolumes = 11,
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

    [Fact]
    public async void TestAuthorDb()
    {
        DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder()
            .UseNpgsql("Server=localhost;Database=testDbBooks;Username=postgres;Password=1234");

        await using (var db = new ApplicationDbContext(dbContextOptionsBuilder.Options))
        {
            LibraryComic? firstOrDefault = db.LibraryComics
                .Include(x => x.Authors)
                .FirstOrDefault(x => x.Id == Guid.Parse("ac63855c-7ac5-4a12-bfb0-49ac35def588"));

            Console.WriteLine();
        }
    }

    [Fact]
    public async void TestLibraryComicWithNewAuthor() // Use this for create library comic
    {
        // AuthorRequest authorRequest = new AuthorRequest
        // {
        //     FirstName = "firstName",
        //     LastName = "lastName",
        //     Role = "story"
        // };
        // AuthorRequest authorRequest2 = new AuthorRequest
        // {
        //     FirstName = "firstName2",
        //     LastName = "lastName2",
        //     Role = "story"
        // };

        Author authorRequest = new Author
        {
            Id = Guid.Parse("83c48659-4ded-49cd-96d7-09ee8ba60739"),
            FirstName = "firstName",
            LastName = "lastName",
            Role = "story",
        };
        Author authorRequest2 = new Author
        {
            Id = Guid.Parse("cec19186-48a6-487c-a1cc-d43e9c22d86c"),
            FirstName = "firstName2",
            LastName = "lastName2",
            Role = "art"
        };

        CreateLibraryComicRequest request = new CreateLibraryComicRequest
        {
            Title = "string",
            DemographicType = "Seinen",
            ComicType = "Manga",
            PublishingStatus = "Publishing",
            TotalVolumes = 121,
            // Authors = new List<AuthorRequest>()
            // {
            //     authorRequest, authorRequest2
            // }
        };


        MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
        {
            config.AddProfile(new LibraryComicProfile());
            config.AddProfile(new AuthorProfile());
        });

        IMapper mapper = mapperConfiguration.CreateMapper();

        LibraryComic libraryComic = mapper.Map<LibraryComic>(request);

        Console.WriteLine();


        DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder()
            .UseNpgsql("Server=localhost;Database=testDbBooks;Username=postgres;Password=1234");

        List<Author> authors = new List<Author>() { authorRequest, authorRequest2};

        await using (var db = new ApplicationDbContext(dbContextOptionsBuilder.Options))
        {
            foreach (Author author in authors)
            {
                Author? searchedAuthor = await db.Authors.FirstOrDefaultAsync(a => a.FirstName == author.FirstName &&
                    a.LastName == author.LastName &&
                    a.Role == author.Role);

                if (searchedAuthor == null)
                {
                    libraryComic.Authors.Add(author);
                }
                else
                {
                    libraryComic.Authors.Add(searchedAuthor);
                }
            }

            db.LibraryComics.Add(libraryComic);
            await db.SaveChangesAsync();
            
        }
    }
    
    [Fact]
    public async void TestLibraryComicWithNewAuthorUpdate() // Use this for update libraryComic
    {

        Author authorRequest = new Author
        {
            Id = Guid.Parse("83c48659-4ded-49cd-96d7-09ee8ba60739"),
            FirstName = "firstName",
            LastName = "lastName",
            Role = "story",
        };
        Author authorRequest2 = new Author
        {
            Id = Guid.Parse("cec19186-48a6-487c-a1cc-d43e9c22d86c"),
            FirstName = "firstName2",
            LastName = "lastName2",
            Role = "art"
        };
        Author authorRequest3 = new Author
        {
            Id = Guid.Parse("039e9297-3d31-44a2-b899-79f3f164eb62"),
            FirstName = "firstName3",
            LastName = "lastName3",
            Role = "storyAndArt"
        };
        

        MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
        {
            config.AddProfile(new LibraryComicProfile());
            config.AddProfile(new AuthorProfile());
        });
        
        
        
        Console.WriteLine();
        
        DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder()
            .UseNpgsql("Server=localhost;Database=testDbBooks;Username=postgres;Password=1234");

        List<Author> authors = new List<Author>() { authorRequest, authorRequest3};

        await using (var db = new ApplicationDbContext(dbContextOptionsBuilder.Options))
        {
            LibraryComic? libraryComic = await db.LibraryComics
                .Include(x => x.Authors)
                .Include(x => x.UserComics)
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse("a1d27d10-8d31-4479-acc4-b35919b0c1d7"));
            if (libraryComic == null)
            {
                return;
            }

            libraryComic.ComicType = "Seinen";
            
            libraryComic.Authors.Clear();
            
            IMapper mapper = mapperConfiguration.CreateMapper();

            UpdateLibraryComicRequest request = new UpdateLibraryComicRequest
            {
                Title = "title",
                DemographicType = "Shonen",
                ComicType = "OneShot",
                PublishingStatus = "OnHiatus",
                TotalVolumes = 11,
                Authors = new List<AuthorRequest>()
            };

            mapper.Map(request, libraryComic);
            
            
            foreach (Author author in authors)
            {
                Author? searchedAuthor = await db.Authors.FirstOrDefaultAsync(a => a.FirstName == author.FirstName &&
                    a.LastName == author.LastName &&
                    a.Role == author.Role);

                if (searchedAuthor == null)
                {
                    libraryComic.Authors.Add(author);
                    db.Authors.Add(author);
                }
                else
                {
                    libraryComic.Authors.Add(searchedAuthor);
                }
            }
            
            db.Entry(libraryComic).State = EntityState.Modified;
            await db.SaveChangesAsync();
            
        }
    }
}