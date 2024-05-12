using AutoMapper;
using BooksAPI.FE.Contracts.Author;
using BooksAPI.FE.Contracts.LibraryManga;
using BooksAPI.FE.Model;

namespace BooksAPI.FE.Mapping;

public class LibraryMangaProfile : Profile
{
    public LibraryMangaProfile()
    {
        CreateMap<LibraryMangaModel, CreateLibraryMangaRequest>();
        CreateMap<LibraryMangaModel, UpdateLibraryMangaRequest>();
        CreateMap<LibraryMangaResponse, LibraryMangaModel>();


        //Add authors here
        CreateMap<AuthorModel, AuthorRequest>();
        CreateMap<AuthorResponse, AuthorModel>();
    }
}