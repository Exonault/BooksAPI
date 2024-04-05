using BooksAPI.BE.Data;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.BE.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AuthorRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAuthor(Author author)
    {
        await _dbContext.Authors.AddAsync(author);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Author?> GetAuthorById(Guid id)
    {
        return await _dbContext.Authors.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Author?> GetAuthorByName(string firstName, string lastName)
    {
        return await _dbContext.Authors.FirstOrDefaultAsync(a => a.FirstName == firstName && a.LastName == lastName);
    }

    public async Task<List<Author>> GetAllAuthors()
    {
        return await _dbContext.Authors.ToListAsync();
    }

    public async Task UpdateAuthor(Author author)
    {
        _dbContext.Entry(author).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAuthor(Author author)
    {
        _dbContext.Authors.Remove(author);
        await _dbContext.SaveChangesAsync();
    }
}