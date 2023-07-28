using BooksAPI.Entities;

namespace BooksAPI.Interfaces.Repositories;

public interface IComicRepository
{
    public Task<Comic> CreateComic(Comic comic);

    public Task<Comic?> GetComicById(Guid id);

    public Task<List<Comic>> GetAllComics();

    public Task<Comic> UpdateComic(Comic comic);

    public Task DeleteComic(Comic comic);
}