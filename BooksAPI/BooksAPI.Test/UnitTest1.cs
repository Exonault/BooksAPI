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
            config.AddProfile(new UserMangaProfile());
            config.AddProfile(new LibraryMangaProfile());
        });

        IMapper mapper = mapperConfiguration.CreateMapper();

        CreateUserMangaRequest request = new CreateUserMangaRequest
        {
            ReadingStatus = "Reading",
            ReadVolumes = 12,
            CollectedVolumes = 12,
            Price = 0,
            CollectionStatus = "Collecting",
            UserId = "2f9f94b0-2b87-4892-b95e-5377df0cc42b",
            LibraryMangaId = Guid.Parse("323c9333-c943-41c8-932e-3dc295eda791")
        };

        UserManga userManga = mapper.Map<UserManga>(request);
        //Doesnt map user and libraryComic (manual mapping needs to be done)

        Console.WriteLine();
    }

    [Fact]
    public async void TestMappingResponse()
    {
        MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
        {
            config.AddProfile(new UserMangaProfile());
        });

        IMapper mapper = mapperConfiguration.CreateMapper();

        DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder()
            .UseNpgsql("Server=localhost;Database=testDbBooks;Username=postgres;Password=1234");

        UserManga userManga;
        await using (var db = new ApplicationDbContext(dbContextOptionsBuilder.Options))
        {
            User user = db.Users.FirstOrDefault(x => true)!;
            LibraryManga libraryManga = db.LibraryMangas.FirstOrDefault(x => true)!;

            userManga = new UserManga
            {
                Id = Guid.NewGuid(),
                ReadingStatus = "Reading",
                ReadVolumes = 11,
                CollectedVolumes = 10,
                PricePerVolume = 18,
                CollectionStatus = "Collecting",
                User = user,
                LibraryManga = libraryManga
            };
        }

        UserMangaResponse userMangaResponse = mapper.Map<UserMangaResponse>(userManga);
        //Maps UserId but not the LibraryComicResponse
        Console.WriteLine();
    }

    [Fact]
    public async void TestValidation()
    {
        UserMangaValidator userMangaValidator = new UserMangaValidator();

        DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder()
            .UseNpgsql("Server=localhost;Database=testDbBooks;Username=postgres;Password=1234");
        UserManga userManga;

        await using (var db = new ApplicationDbContext(dbContextOptionsBuilder.Options))
        {
            User user = db.Users.FirstOrDefault(x => true)!;
            //LibraryComic libraryComic = db.LibraryComics.FirstOrDefault(x => true)!;

            LibraryManga libraryManga = new LibraryManga()
            {
                Id = Guid.NewGuid(),
                Title = "null",
                //Author = "null",
                DemographicType = "null",
                Type = "null",
                PublishingStatus = "null",
                TotalVolumes = 0,
            };


            userManga = new UserManga
            {
                Id = Guid.NewGuid(),
                ReadingStatus = "Reading",
                ReadVolumes = 11,
                CollectedVolumes = 10,
                PricePerVolume = 18,
                CollectionStatus = "Collecting",
                User = user,
                LibraryManga = libraryManga
            };
        }

        ValidationResult validationResult = await userMangaValidator.ValidateAsync(userManga);

        Console.WriteLine(validationResult.Errors);
    }

    [Fact]
    public async void TestDb()
    {
        DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder()
            .UseNpgsql("Server=localhost;Database=testDbBooks;Username=postgres;Password=1234");

        await using (var db = new ApplicationDbContext(dbContextOptionsBuilder.Options))
        {
            UserManga? firstOrDefaultAsync = await db.UserMangas
                .Include(x => x.User)
                .Include(x => x.LibraryManga)
                .FirstOrDefaultAsync();

            Console.WriteLine();
        }
    }

    [Fact]
    public async void TestMapping()
    {
        MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
        {
            config.AddProfile(new UserMangaProfile());
        });

        IMapper mapper = mapperConfiguration.CreateMapper();


        DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder()
            .UseNpgsql("Server=localhost;Database=testDbBooks;Username=postgres;Password=1234");

        UserManga userManga;
        UpdateUserMangaRequest updateUserMangaRequest = new UpdateUserMangaRequest
        {
            ReadingStatus = "Reading",
            ReadVolumes = 1211,
            CollectedVolumes = 111,
            Price = 112.11m,
            CollectionStatus = "Finished",
            UserId = "",
            LibraryMangaId = Guid.NewGuid()
        };

        await using (var db = new ApplicationDbContext(dbContextOptionsBuilder.Options))
        {
            UserManga? uc = await db.UserMangas
                .Include(x => x.User)
                .Include(x => x.LibraryManga)
                .FirstOrDefaultAsync();

            mapper.Map(updateUserMangaRequest, uc);

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
            LibraryManga? firstOrDefault = db.LibraryMangas
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

        CreateLibraryMangaRequest request = new CreateLibraryMangaRequest
        {
            Title = "string",
            DemographicType = "Seinen",
            Type = "Manga",
            PublishingStatus = "Publishing",
            TotalVolumes = 121,
            // Authors = new List<AuthorRequest>()
            // {
            //     authorRequest, authorRequest2
            // }
        };


        MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
        {
            config.AddProfile(new LibraryMangaProfile());
            config.AddProfile(new AuthorProfile());
        });

        IMapper mapper = mapperConfiguration.CreateMapper();

        LibraryManga libraryManga = mapper.Map<LibraryManga>(request);

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
                    libraryManga.Authors.Add(author);
                }
                else
                {
                    libraryManga.Authors.Add(searchedAuthor);
                }
            }

            db.LibraryMangas.Add(libraryManga);
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
            config.AddProfile(new LibraryMangaProfile());
            config.AddProfile(new AuthorProfile());
        });
        
        
        
        Console.WriteLine();
        
        DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder()
            .UseNpgsql("Server=localhost;Database=testDbBooks;Username=postgres;Password=1234");

        List<Author> authors = new List<Author>() { authorRequest, authorRequest3};

        await using (var db = new ApplicationDbContext(dbContextOptionsBuilder.Options))
        {
            LibraryManga? libraryComic = await db.LibraryMangas
                .Include(x => x.Authors)
                .Include(x => x.UserMangas)
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse("a1d27d10-8d31-4479-acc4-b35919b0c1d7"));
            if (libraryComic == null)
            {
                return;
            }

            libraryComic.Type = "Seinen";
            
            libraryComic.Authors.Clear();
            
            IMapper mapper = mapperConfiguration.CreateMapper();

            UpdateLibraryMangaRequest request = new UpdateLibraryMangaRequest
            {
                Title = "title",
                DemographicType = "Shonen",
                Type = "OneShot",
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
    
    [Fact]
    public async void TestDb2()
    {
        DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder()
            .UseNpgsql("Server=localhost;Database=booksApi;Username=postgres;Password=1234");

        await using (var db = new ApplicationDbContext(dbContextOptionsBuilder.Options))
        {
            UserManga? firstOrDefaultAsync = await db.UserMangas.FirstOrDefaultAsync();

            if (firstOrDefaultAsync is not null)
            {
                _testOutputHelper.WriteLine(firstOrDefaultAsync.PricePerVolume.ToString());    
            }
            
        }
    }

}