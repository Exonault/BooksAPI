using AutoMapper;
using BooksAPI.BE.Contracts.UserComic;
using BooksAPI.BE.Entities;

namespace BooksAPI.BE.Mapping;

public class UserMangaProfile:Profile
{
    public UserMangaProfile()
    {
        CreateMap<CreateUserMangaRequest, UserManga>(); //Doesn't map LibraryComic and User

        CreateMap<UserManga, UserMangaResponse>(); // Doesn't map LibraryComic
 
        CreateMap<UpdateUserMangaRequest, UserManga>(); //Doesn't map LibraryComic and User
    }
}