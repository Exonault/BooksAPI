using BooksAPI.FE.Contracts.UserManga;
using BooksAPI.FE.Model;

namespace BooksAPI.FE.Interfaces;

public interface IUserMangaService
{
    Task<IEnumerable<UserMangaResponse>> GetUserMangas(string token, string refreshToken, string userId);
    Task<UserMangaResponse> GetUserManga(string token, string refreshToken, string userId);
    Task<bool> CreateUserManga(UserMangaModel model, string token, string refreshToken, string userId);
    Task<bool> UpdateUserManga(UserMangaModel model, string token, string refreshToken, string userId);
    Task<bool> DeleteUserManga(UserMangaModel model, string token, string refreshToken, string userId);
}