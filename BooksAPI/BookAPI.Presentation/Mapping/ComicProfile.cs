using AutoMapper;
using BookAPI.Presentation.Contracts.Requests.Comic;
using BookAPI.Presentation.Contracts.Response.Comic;
using BookAPI.Presentation.Models;

namespace BookAPI.Presentation.Mapping;

public class ComicProfile : Profile
{
    public ComicProfile()
    {
        CreateMap<GetComicResponse, ComicsListElementModel>();

        CreateMap<GetComicResponse, ModifyComicsModel>();
        CreateMap<ModifyComicsModel, CreateComicRequest>();

        CreateMap<ModifyComicsModel, UpdateComicRequest>();
    }
}