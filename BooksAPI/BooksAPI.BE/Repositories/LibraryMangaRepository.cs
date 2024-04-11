using BooksAPI.BE.Data;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.BE.Repositories;

public class LibraryMangaRepository:ILibraryMangaRepository
{
    private readonly ApplicationDbContext _dbContext;

    public LibraryMangaRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateLibraryManga(LibraryManga libraryManga)
    {
        await _dbContext.LibraryMangas.AddAsync(libraryManga);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<LibraryManga?> GetLibraryMangaById(Guid id)
    {
        return await _dbContext.LibraryMangas
            .Include(lm => lm.Authors)
            .FirstOrDefaultAsync(lm => lm.Id == id);
    }

    public async Task<List<LibraryManga>> GetAllLibraryMangas()
    {
        return await _dbContext.LibraryMangas
            .Include(lm => lm.Authors)
            .ToListAsync();
    }

    public async Task<List<LibraryManga>> GetLibraryMangasForPage(int pageIndex, int pageEntriesCount)
    {
        return await _dbContext.LibraryMangas
            .Include(lm => lm.Authors)
            .Skip(pageIndex * pageEntriesCount)
            .Take(pageEntriesCount)
            .ToListAsync();
    }

    public async Task UpdateLibraryManga(LibraryManga libraryManga)
    {
        _dbContext.Entry(libraryManga).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteLibraryManga(LibraryManga libraryManga)
    {
        _dbContext.LibraryMangas.Remove(libraryManga);
        await _dbContext.SaveChangesAsync();
    }
}