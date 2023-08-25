using BookAPI.Presentation.Models;

namespace BookAPI.Presentation.Interfaces;

public interface IComicsService
{
    Task<HttpResponseMessage> CreateComic(ModifyComicsModel model);

    Task<IEnumerable<ComicsListElementModel>> GetAllComics();

    Task<ModifyComicsModel> GetComic(String id);

    Task<HttpResponseMessage> UpdateComic(String id, ModifyComicsModel model);

    Task<HttpResponseMessage> DeleteComic(String id);
}