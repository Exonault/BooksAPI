using BooksAPI.BE.Entities;

namespace BooksAPI.BE.Interfaces.Repositories;

public interface IAuthorRepository
{
    public Task CreateAuthor(Author author);

    public Task<Author?> GetAuthorById(Guid id);
    
    public Task<Author?> GetAuthorByName(string firstName, string lastName);

    public Task<List<Author>> GetAllAuthors();

    public Task UpdateAuthor(Author author);

    public Task DeleteAuthor(Author author);
}