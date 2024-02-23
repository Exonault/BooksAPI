using BooksAPI.BE.Data;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.BE.Repositories;

public class UserComicRepository:IUserComicRepository
{

    private readonly ApplicationDbContext _dbContext;

    public UserComicRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateUserComic(UserComic userComic)
    {
        await _dbContext.UserComics.AddAsync(userComic);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserComic?> GetUserComicById(Guid id)
    {
       return await _dbContext.UserComics
           .Include(uc=> uc.LibraryComic)
           .Include(uc=>uc.User)
           .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<UserComic>> GetAllUserComic()
    {
        return await _dbContext.UserComics
            .Include(uc=> uc.LibraryComic)
            .Include(uc=>uc.User)
            .ToListAsync();
    }

    public async Task UpdateUserComic(UserComic userComic)
    {
        _dbContext.Entry(userComic).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUserComic(UserComic userComic)
    {
        _dbContext.UserComics.Remove(userComic);
        await _dbContext.SaveChangesAsync();
    }
}