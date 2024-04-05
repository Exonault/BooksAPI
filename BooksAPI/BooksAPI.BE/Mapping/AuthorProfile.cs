using AutoMapper;
using BooksAPI.BE.Contracts.Author;
using BooksAPI.BE.Entities;

namespace BooksAPI.BE.Mapping;

public class AuthorProfile:Profile
{
    public AuthorProfile()
    {
        CreateMap<AuthorRequest, Author>();
        CreateMap<Author, AuthorResponse>();
    }
    
}