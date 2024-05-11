namespace BooksAPI.FE.Contracts.Author;

public class AuthorRequest
{
    public string? FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
    
    public string Role { get; set; } = string.Empty;
}