using BookAPI.Presentation.Models;

namespace BookAPI.Presentation.Interfaces;

public interface IComicsService
{
    Task<HttpResponseMessage> CreateComic(ModifyComicsModel model);

    Task<HttpResponseMessage> GetAllComics();

    Task<HttpResponseMessage> GetComic(string id);

    Task<HttpResponseMessage> UpdateComic(string id, ModifyComicsModel model);

    Task<HttpResponseMessage> DeleteComic(string id);
}