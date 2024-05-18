using AutoMapper;
using BooksAPI.FE.Contracts.UserManga;
using BooksAPI.FE.Model;

namespace BooksAPI.FE.Mapping;

public class UserMangaProfile : Profile
{
    public UserMangaProfile()
    {
        CreateMap<UserMangaModel, CreateUserMangaRequest>();
        CreateMap<UserMangaModel, UpdateUserMangaRequest>();
        CreateMap<UserMangaResponse, UserMangaModel>();
    }
}