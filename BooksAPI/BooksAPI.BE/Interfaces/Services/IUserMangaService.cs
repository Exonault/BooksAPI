using BooksAPI.BE.Contracts.UserComic;

namespace BooksAPI.BE.Interfaces.Services;

public interface IUserMangaService
{
    public Task CreateUserManga(CreateUserMangaRequest request);

    public Task<UserMangaResponse> GetUserManga(Guid id);

    public Task<List<UserMangaResponse>> GetAllUserMangas();

    public Task<List<UserMangaResponse>> GetAllUserMangasByUserId(string id);

    public Task UpdateUserManga(Guid id, UpdateUserMangaRequest request);
    
    public Task DeleteUserManga(Guid id, string userId);
}