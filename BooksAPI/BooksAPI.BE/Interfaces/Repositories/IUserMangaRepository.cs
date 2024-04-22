using BooksAPI.BE.Entities;

namespace BooksAPI.BE.Interfaces.Repositories;

public interface IUserMangaRepository
{
    public Task CreateUserManga(UserManga libraryManga);

    public Task<UserManga?> GetUserMangaById(int id);

    public Task<List<UserManga>> GetUserMangaByUserId(string userId);

    public Task<List<UserManga>> GetAllUserManga();
    
    public Task UpdateUserManga(UserManga userManga);

    public Task DeleteUserManga(UserManga userManga);
}