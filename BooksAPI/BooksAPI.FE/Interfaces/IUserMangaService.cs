using BooksAPI.FE.Contracts.UserManga;

namespace BooksAPI.FE.Interfaces;

public interface IUserMangaService
{
    public Task<IEnumerable<UserMangaResponse>> GetUserMangas(string token, string refreshToken, string userId);
}