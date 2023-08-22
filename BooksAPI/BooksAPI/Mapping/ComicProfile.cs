using AutoMapper;
using BooksAPI.Contracts.Requests.Comic;
using BooksAPI.Contracts.Response.Comic;
using BooksAPI.Entities;

namespace BooksAPI.Mapping;

public class ComicProfile : Profile
{
    public ComicProfile()
    {
        CreateMap<CreateComicRequest, Comic>();
       // CreateMap<Comic, CreateComicResponse>();

        CreateMap<Comic, GetComicResponse>();

        CreateMap<UpdateComicRequest, Comic>();
       // CreateMap<Comic, UpdateComicResponse>();

       // CreateMap<Comic, DeleteComicResponse>();
    }
}