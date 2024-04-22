using BooksAPI.BE.Data;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.BE.Repositories;

public class UserMangaRepository : IUserMangaRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserMangaRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateUserManga(UserManga userManga)
    {
        await _dbContext.UserMangas.AddAsync(userManga);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserManga?> GetUserMangaById(int id)
    {
        return await _dbContext.UserMangas
            .Include(um => um.LibraryManga)
            .Include(um => um.LibraryManga.Authors)
            .Include(um => um.User)
            .FirstOrDefaultAsync(um => um.Id == id);
    }

    public async Task<List<UserManga>> GetUserMangaByUserId(string userId)
    {
        return await _dbContext.UserMangas
            .Include(um => um.LibraryManga)
            .Include(um => um.LibraryManga.Authors)
            .Include(um => um.User)
            .Where(um => um.User.Id == userId)
            .OrderBy(um => um.Id)
            .ToListAsync();
    }

    public async Task<List<UserManga>> GetAllUserManga()
    {
        return await _dbContext.UserMangas
            .Include(um => um.LibraryManga)
            .Include(um => um.LibraryManga.Authors)
            .Include(um => um.User)
            .OrderBy(um => um.Id)
            .ToListAsync();
    }

    public async Task UpdateUserManga(UserManga userManga)
    {
        _dbContext.Entry(userManga).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUserManga(UserManga userManga)
    {
        _dbContext.UserMangas.Remove(userManga);
        await _dbContext.SaveChangesAsync();
    }
}