using BooksAPI.FE.Contracts.User;
using BooksAPI.FE.Model;

namespace BooksAPI.FE.Interfaces;

public interface IUserService
{
    public Task<RegisterResponse?> Register(RegisterModel model);

    public Task<LoginResponse?> Login(LoginModel model);
    
    // public Task Refresh();
}