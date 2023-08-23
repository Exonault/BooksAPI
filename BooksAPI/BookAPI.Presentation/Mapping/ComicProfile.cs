using AutoMapper;
using BookAPI.Presentation.Contracts.Response.Comic;
using BookAPI.Presentation.Models;

namespace BookAPI.Presentation.Mapping;

public class ComicProfile: Profile
{
    public ComicProfile()
    {
        CreateMap<GetComicResponse, CreateComicsModel>();
    }
}