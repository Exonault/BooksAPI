using AutoMapper;
using BooksAPI.BE.Contracts.UserComic;
using BooksAPI.BE.Entities;

namespace BooksAPI.BE.Mapping;

public class UserComicProfile:Profile
{
    public UserComicProfile()
    {
        CreateMap<CreateUserComicRequest, UserComic>(); //Doesn't map LibraryComic and User

        CreateMap<UserComic, UserComicResponse>(); // Doesn't map LibraryComic
 
        CreateMap<UpdateUserComicRequest, UserComic>(); //Doesn't map LibraryComic and User
    }
}