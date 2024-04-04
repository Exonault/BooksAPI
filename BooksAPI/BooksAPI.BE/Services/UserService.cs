using BooksAPI.BE.Contracts.User;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Interfaces.Repositories;
using BooksAPI.BE.Interfaces.Services;
using BooksAPI.BE.Messages;

namespace BooksAPI.BE.Services;


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
            throw new ArgumentException(UserMessages.ValidationMessages.EmptyRequest);
        }

        User newUser = new User()
        { 
            UserName =  request.UserName,
            PasswordHash = request.Password,
            Email = request.Email
        };

        await _userRepository.Register(newUser, request.Admin);

        return new RegisterResponse
        {
            Message = UserMessages.Messages.AccountCreated,
            Successful = true,
            Errors = new List<string>(),
        };
        // return new RegisterResponse(UserMessages.AccountCreated, null);
    }

    public async Task<LoginResponse> LoginAccount(LoginRequest request)
    {
        if (request is null)
        {
            throw new ArgumentException(UserMessages.ValidationMessages.EmptyRequest);
        }

        String token = await _userRepository.Login(request.UserName, request.Password);

        return new LoginResponse
        {
            Token = token,
            RefreshToken = token,
            Message = UserMessages.Messages.LoginComplete
        };
        //     return new LoginResponse(token, token, UserMessages.LoginComplete);
    }

    public Task Refresh(RefreshRequest request)
    {
        throw new NotImplementedException();
    }

    public Task Revoke()
    {
        throw new NotImplementedException();
    }
}