using BooksAPI.FE.Contracts.UserManga;
using BooksAPI.FE.Model;

namespace BooksAPI.FE.Interfaces;

public interface IUserMangaService
{
    Task<List<UserMangaResponse>> GetUserMangas(string token, string refreshToken, string userId);
    Task<UserMangaModel> GetUserMangaModel(int id, string token, string refreshToken, string userId);
    Task<UserMangaResponse> GetUserManga(int id, string token, string refreshToken, string userId);
    Task<bool> CreateUserManga(UserMangaModel model, string token, string refreshToken, string userId);
    Task<bool> UpdateUserManga(int id, UserMangaModel model, string token, string refreshToken, string userId);
    Task<bool> DeleteUserManga(int id, string token, string refreshToken, string userId);
}