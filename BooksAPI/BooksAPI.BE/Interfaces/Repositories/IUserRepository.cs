using BooksAPI.BE.Entities;

namespace BooksAPI.BE.Interfaces.Repositories;

public interface IUserRepository
{
    Task Register(User newUser, bool admin);
    
    Task<string> Login(string email, string password);
}