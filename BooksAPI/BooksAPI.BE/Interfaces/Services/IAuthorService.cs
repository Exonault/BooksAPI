using BooksAPI.BE.Contracts.Author;

namespace BooksAPI.BE.Interfaces.Services;

public interface IAuthorService
{
    public Task CreateAuthor(CreateAuthorRequest request);

    public Task<AuthorResponse> GetAuthorById(Guid id);
    
    public Task<AuthorResponse> GetAuthorByName(string firstName, string lastName);

    public Task<List<AuthorResponse>> GetAllAuthors();

    public Task UpdateAuthor(Guid id, UpdateAuthorRequest request);
    
    public Task DeleteAuthor(Guid id);
}