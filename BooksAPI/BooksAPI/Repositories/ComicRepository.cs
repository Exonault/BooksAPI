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


    public async Task CreateComic(Comic comic)
    {
        await _dbContext.Comics.AddAsync(comic);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Comic?> GetComicById(Guid id)
    {
        return await _dbContext.Comics.FindAsync(id);
    }

    public async Task<List<Comic>> GetAllComics()
    {
        return await _dbContext.Comics.ToListAsync();
    }

    public async Task<List<Comic>> GetAllComicsByReadingStatus(String readingStatus)
    {
        return await _dbContext.Comics.Where(x => x.ReadingStatus == readingStatus).ToListAsync();
    }

    public async Task<List<Comic>> GetAllComicsByDemographic(String demographic)
    {
        return await _dbContext.Comics.Where(x => x.DemographicType == demographic).ToListAsync();
    }

    public async Task<List<Comic>> GetAllComicsByPublishingStatus(String publishingStatus)
    {
        return await _dbContext.Comics.Where(x => x.PublishingStatus == publishingStatus).ToListAsync();
    }

    public async Task<List<Comic>> GetAllComicsByComicType(String comicType)
    {
        return await _dbContext.Comics.Where(x => x.ComicType == comicType).ToListAsync();
    }

    public async Task UpdateComic(Comic comic)
    {
        _dbContext.Entry(comic).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteComic(Comic comic)
    {
        _dbContext.Comics.Remove(comic);
        await _dbContext.SaveChangesAsync();
    }
}