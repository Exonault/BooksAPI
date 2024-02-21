using BooksAPI.BE.Entities;

namespace BooksAPI.BE.Interfaces.Repositories;

public interface IUserComicRepository
{
    public Task CreateUserComic(UserComic libraryComic);

    public Task<UserComic?> GetUserComicById(Guid id);

    public Task<List<UserComic>> GetAllUserComic();
    
    public Task UpdateUserComic(UserComic userComic);

    public Task DeleteUserComic(UserComic userComic);
}