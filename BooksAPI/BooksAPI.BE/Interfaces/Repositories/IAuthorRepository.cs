using BooksAPI.BE.Entities;

namespace BooksAPI.BE.Interfaces.Repositories;

public interface IAuthorRepository
{
    Task CreateAuthor(Author author);

    Task<Author?> GetAuthorById(int id);

    Task<Author?> GetAuthorByName(string firstName, string lastName);
   
    Task<Author?> GetAuthor(string firstName, string lastName, string role);
    
    Task<List<Author>> GetAllAuthors();
    Task UpdateAuthor(Author author);

    Task DeleteAuthor(Author author);
}