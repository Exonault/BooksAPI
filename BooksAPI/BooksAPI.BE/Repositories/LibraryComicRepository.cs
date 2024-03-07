using BooksAPI.BE.Data;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.BE.Repositories;

public class LibraryComicRepository:ILibraryComicRepository
{
    private readonly ApplicationDbContext _dbContext;

    public LibraryComicRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateLibraryComic(LibraryComic libraryComic)
    {
        await _dbContext.LibraryComics.AddAsync(libraryComic);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<LibraryComic?> GetLibraryComicById(Guid id)
    {
        return await _dbContext.LibraryComics.FirstOrDefaultAsync(lc => lc.Id == id);
    }

    public async Task<List<LibraryComic>> GetAllLibraryComics()
    {
        return await _dbContext.LibraryComics.ToListAsync();
    }

    public async Task UpdateLibraryComic(LibraryComic libraryComic)
    {
        _dbContext.Entry(libraryComic).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteLibraryComic(LibraryComic libraryComic)
    {
        _dbContext.LibraryComics.Remove(libraryComic);
        await _dbContext.SaveChangesAsync();
    }
}