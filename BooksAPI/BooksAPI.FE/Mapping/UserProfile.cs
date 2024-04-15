using AutoMapper;
using BooksAPI.FE.Contracts.User;
using BooksAPI.FE.Model;

namespace BooksAPI.FE.Mapping;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<RegisterModel, RegisterRequest>();

        CreateMap<LoginModel, LoginRequest>();
    }
}