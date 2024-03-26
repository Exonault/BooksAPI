using BooksAPI.BE.Contracts.UserComic;

namespace BooksAPI.BE.Interfaces.Services;

public interface IUserComicService
{
    public Task CreateUserComic(CreateUserComicRequest request);

    public Task<UserComicResponse> GetUserComic(Guid id);

    public Task<List<UserComicResponse>> GetAllUserComics();

    public Task<List<UserComicResponse>> GetAllUserComicsByUserId(string id);

    public Task UpdateUserComic(Guid id, UpdateUserComicRequest request);
    
    public Task DeleteUserComic(Guid id, string userId);
}