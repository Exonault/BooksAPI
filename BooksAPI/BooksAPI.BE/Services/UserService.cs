using BooksAPI.BE.Contracts.User;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Interfaces.Repositories;
using BooksAPI.BE.Interfaces.Services;
using BooksAPI.BE.Messages;

namespace BooksAPI.BE.Services;

using static UserResponses;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<RegisterResponse> RegisterAccount(RegisterRequest request)
    {
        if (request is null)
        {
            throw new ArgumentException(UserMessages.EmptyRequest);
        }

        User newUser = new User()
        {
            Email = request.Email,
            PasswordHash = request.Password,
            UserName = request.Email
        };

        await _userRepository.Register(newUser, request.Admin);

        return new RegisterResponse(UserMessages.AccountCreated);
    }

    public async Task<LoginResponse> LoginAccount(LoginRequest request)
    {
        if (request is null)
        {
            throw new ArgumentException(UserMessages.EmptyRequest);
        }

        String token = await _userRepository.Login(request.Email, request.Password);

        return new LoginResponse(token, UserMessages.LoginComplete);
    }
}