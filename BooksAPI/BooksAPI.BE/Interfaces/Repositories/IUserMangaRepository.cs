using BooksAPI.BE.Entities;

namespace BooksAPI.BE.Interfaces.Repositories;

public interface IUserMangaRepository
{
    Task CreateUserManga(UserManga libraryManga);

    Task<UserManga?> GetUserMangaById(int id);

    Task<List<UserManga>> GetUserMangaByUserId(string userId);

    Task<List<UserManga>> GetAllUserManga();

    Task UpdateUserManga(UserManga userManga);

    Task DeleteUserManga(UserManga userManga);
}