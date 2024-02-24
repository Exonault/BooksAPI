using AutoMapper;
using BooksAPI.BE.Contracts.LibraryComic;
using BooksAPI.BE.Entities;

namespace BooksAPI.BE.Mapping;

public class LibraryComicProfile:Profile
{
    public LibraryComicProfile()
    {
        CreateMap<CreateLibraryComicRequest, LibraryComic>();

        CreateMap<LibraryComic, LibraryComicResponse>();

        CreateMap<UpdateLibraryComicRequest, LibraryComic>();
    }
}