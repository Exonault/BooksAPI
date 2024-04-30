using BooksAPI.BE.Contracts.UserManga;

namespace BooksAPI.BE.Interfaces.Services;

public interface IUserMangaService
{
    public Task CreateUserManga(CreateUserMangaRequest request);

    public Task<UserMangaResponse> GetUserManga(int id);

    public Task<List<UserMangaResponse>> GetAllUserMangas();

    public Task<List<UserMangaResponse>> GetAllUserMangasByUserId(string id);

    public Task UpdateUserManga(int id, UpdateUserMangaRequest request);
    
    public Task DeleteUserManga(int id, string userId);
}