using BooksAPI.BE.Constants;
using BooksAPI.BE.Contracts.User;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Exception;
using BooksAPI.BE.Interfaces.Repositories;
using BooksAPI.BE.Interfaces.Services;
using BooksAPI.BE.Messages;
using BooksAPI.BE.Repositories;

namespace BooksAPI.BE.Services;

using static UserResponses;

public class UserService1 : IUserService1
{
    private IUserRepository1 _repository;

    public UserService1(UserRepository1 repository)
    {
        _repository = repository;
    }

    public async Task<RegisterResponse> RegisterAccount(RegisterRequest request)
    {
        if (request is null)
        {
            throw new ArgumentException(UserMessages.EmptyRequest);
        }

        User? userByName = await _repository.GetByEmail(request.Email);

        if (userByName is not null)
        {
            throw new UserAlreadyRegisteredException(UserMessages.AlreadyRegistered);
        }

        User user = new User()
        {
            UserName = request.UserName,
            PasswordHash = request.Password,
            Email = request.Email
        };

        bool created = await _repository.Create(user, request.Password);

        if (!created)
        {
            throw new System.Exception(UserMessages.ErrorOccured);
        }

        if (request.Admin)
        {
            await _repository.AddToRole(user, AppConstants.RoleTypes.AdminRoleType);
        }

        await _repository.AddToRole(user, AppConstants.RoleTypes.UserRoleType);

        return new RegisterResponse(UserMessages.AccountCreated);
    }

    public async Task<LoginResponse> LoginAccount(LoginRequest request)
    {
        if (request is null)
        {
            throw new ArgumentException(UserMessages.EmptyRequest);
        }

        User? user = await _repository.GetByName(request.UserName);

        if (user is null)
        {
            throw new UserNotFoundException(UserMessages.UserNotFound);
        }

        bool checkPassword = await _repository.CheckPassword(user, request.Password);

        if (!checkPassword)
        {
            throw new InvalidEmailPasswordException(UserMessages.InvalidEmailPassword);
        }

        List<string> roles = await _repository.GetAllRoles(user);
        

        //generate token
        
        //generate refresh token
        
        
        throw new NotImplementedException();
    }

    public Task Refresh(RefreshRequest request)
    {
        throw new NotImplementedException();
    }

    public Task Revoke()
    {
        throw new NotImplementedException();
    }

    private string GenerateToken()
    {
        return null;
    }


    private string GenerateRefreshToken()
    {
        return null;
    }
    
   // private record UserSession(string Id, string UserName, List<string> Roles);

}