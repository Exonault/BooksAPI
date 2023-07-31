using BooksAPI.Entities;

namespace BooksAPI.Interfaces.Repositories;

public interface IComicRepository
{
    public Task<Comic> CreateComic(Comic comic);

    public Task<Comic?> GetComicById(Guid id);

    public Task<List<Comic>> GetAllComics();

    public Task<List<Comic>> GetAllComicsByReadingStatus(String readingStatus);

    public Task<List<Comic>> GetAllComicsByDemographic(String demographic);

    public Task<List<Comic>> GetAllComicsByPublishingStatus(String publishingStatus);

    public Task<List<Comic>> GetAllComicsByComicType(String comicType);


    public Task<Comic> UpdateComic(Comic comic);

    public Task DeleteComic(Comic comic);
}