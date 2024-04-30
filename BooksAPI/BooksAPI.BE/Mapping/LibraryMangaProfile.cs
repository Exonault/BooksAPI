using AutoMapper;
using BooksAPI.BE.Contracts.LibraryManga;
using BooksAPI.BE.Entities;

namespace BooksAPI.BE.Mapping;

public class LibraryMangaProfile:Profile
{
    public LibraryMangaProfile()
    {
        CreateMap<CreateLibraryMangaRequest, LibraryManga>(); // Doesn't map authors

        CreateMap<LibraryManga, LibraryMangaResponse>(); //Doesn't map authors

        CreateMap<UpdateLibraryMangaRequest, LibraryManga>(); //Doens't map authors
    }
}