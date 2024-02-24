using BooksAPI.BE.Contracts.User;

namespace BooksAPI.BE.Interfaces.Services;
using static UserResponses;

public interface IUserService
{
    Task<RegisterResponse> RegisterAccount(RegisterRequest request);

    Task<LoginResponse> LoginAccount(LoginRequest request);
}