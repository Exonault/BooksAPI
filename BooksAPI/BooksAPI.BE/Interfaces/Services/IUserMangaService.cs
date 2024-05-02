using BooksAPI.BE.Contracts.UserManga;

namespace BooksAPI.BE.Interfaces.Services;

public interface IUserMangaService
{
    Task CreateUserManga(CreateUserMangaRequest request);

    Task<UserMangaResponse> GetUserManga(int id);

    Task<List<UserMangaResponse>> GetAllUserMangas();

    Task<List<UserMangaResponse>> GetAllUserMangasByUserId(string id);

    Task UpdateUserManga(int id, UpdateUserMangaRequest request);

    Task DeleteUserManga(int id, string userId);
}