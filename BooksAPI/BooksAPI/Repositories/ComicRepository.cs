using BooksAPI.Data;
using BooksAPI.Entities;
using BooksAPI.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Repositories;

public class ComicRepository : IComicRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ComicRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Comic> CreateComic(Comic comic)
    {
        await _dbContext.Comics.AddAsync(comic);
        await _dbContext.SaveChangesAsync();
        return comic;
    }

    public async Task<Comic?> GetComicById(Guid id)
    {
        return await _dbContext.Comics.FindAsync(id);
    }

    public async Task<List<Comic>> GetAllComics()
    {
        return await _dbContext.Comics.ToListAsync();
    }

    public async Task<Comic> UpdateComic(Comic comic)
    {
        _dbContext.Entry(comic).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return comic;
    }

    public async Task DeleteComic(Comic comic)
    {
        _dbContext.Comics.Remove(comic);
        await _dbContext.SaveChangesAsync();
    }
}