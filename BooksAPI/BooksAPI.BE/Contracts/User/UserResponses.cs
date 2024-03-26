namespace BooksAPI.BE.Contracts.User;

public static class UserResponses
{
    public record RegisterResponse(string Message);
    
    public record LoginResponse(string Token, string Message);
}