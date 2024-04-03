using BooksAPI.BE.Entities;

namespace BooksAPI.BE.Interfaces.Repositories;

public interface IUserRepository
{
    Task Register(User newUser, bool admin);
    
    Task<string> Login(string userName, string password);

    Task<string> Refresh(string token, string refreshToken);

    Task Revoke();
}