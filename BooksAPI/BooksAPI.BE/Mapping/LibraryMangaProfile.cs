using AutoMapper;
using BooksAPI.BE.Contracts.LibraryManga;
using BooksAPI.BE.Entities;

namespace BooksAPI.BE.Mapping;

public class LibraryMangaProfile:Profile
{
    public LibraryMangaProfile()
    {
        CreateMap<CreateLibraryMangaRequest, LibraryManga>();

        CreateMap<LibraryManga, LibraryMangaResponse>();
        CreateMap<LibraryManga, LibraryMangaForPageResponse>();

        CreateMap<UpdateLibraryMangaRequest, LibraryManga>();

       
        
    }
}