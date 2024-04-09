using AutoMapper;
using BooksAPI.BE.Contracts.LibraryComic;
using BooksAPI.BE.Entities;

namespace BooksAPI.BE.Mapping;

public class LibraryComicProfile:Profile
{
    public LibraryComicProfile()
    {
        CreateMap<CreateLibraryComicRequest, LibraryComic>(); // Doesn't map authors

        CreateMap<LibraryComic, LibraryComicResponse>(); //Doesn't map authors

        CreateMap<UpdateLibraryComicRequest, LibraryComic>(); //Doens't map authors
    }
}